using Bo.Interface.IBusiness;
using Common.Tool;
using ITSTOAPI.Attribute;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Routine.Models.ApiEntityRequest;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity.WeChat;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace ITSTOAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private IConfiguration Configuration { get; }
        private readonly ILog _log;
        private readonly IAppSettingService appSettingService;
        private readonly IChannelService channelService;
        private readonly IOrdersService ordersService;
        private readonly ICustomerService customerService;
        public PaymentController(IConfiguration Configuration, IAppSettingService appSettingService, IChannelService channelService,
            IOrdersService ordersService, ICustomerService customerService)
        {
            this.Configuration = Configuration;
            _log = LogManager.GetLogger(typeof(PaymentController));
            this.appSettingService = appSettingService;
            this.channelService = channelService;
            this.ordersService = ordersService;
            this.customerService = customerService;
        }

        [HttpPost]
        [TypeFilter(typeof(CustomAuthorization))]
        public IActionResult PayForOrders(RequestPayForOrders request)
        {
            ApiBaseResponse response = new ApiBaseResponse();
            #region 校验入参
            if (string.IsNullOrEmpty(request.Brand))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Brand不能为空");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(request.OrderCode))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OrderCode不能为空");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(request.UserCode))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "UserCode不能为空");
                return Ok(response);
            }
            #endregion

            var order = ordersService.GetOrderByOrderCode(request.OrderCode);
            if (order == null)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.ParamError, "OrderCode不存在");
                return Ok(response);
            }

            WechatOrderPayParam payParam = new WechatOrderPayParam
            {
                appid = channelService.GetChannelByBrand(request.Brand).AppId,
                mch_id = Configuration["MerchantNum"],
                device_info = string.Empty,
                nonce_str = Guid.NewGuid().ToString("N"),
                sign = string.Empty,
                sign_type = "MD5",
                body = "在线点餐-门店",
                detail = string.Empty,
                attach = string.Empty,
                out_trade_no = request.OrderCode,
                fee_type = "CNY",
                total_fee = (int)(order?.ActualAmount * 100),
                spbill_create_ip = "127.0.0.1"/*!string.IsNullOrEmpty(HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()) ? HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() : HttpContext.Connection.RemoteIpAddress.ToString()*/,
                time_start = DateTime.Now.ToString("yyyyMMddHHmmss"),
                time_expire = DateTime.Now.AddMinutes(5).ToString("yyyyMMddHHmmss"),
                goods_tag = string.Empty,
                notify_url = Configuration["NotifyUrl"],
                trade_type = "JSAPI",
                product_id = string.Empty,
                limit_pay = string.Empty,
                openid = customerService.GetCustomerChannelByUserCode(request.UserCode).OpenId,
                receipt = string.Empty,
                profit_sharing = string.Empty,
                scene_info = string.Empty,
            };
            _log.Info($"调用微信统一下单接口入参：{JsonConvert.SerializeObject(payParam)}");
            response.ReturnObj = WXOrderPayForHelper.Getprepay(payParam, Configuration["PartnerKey"], appSettingService.GetAppSettingByKey(request.Brand, "WeChatPayForUrl")?.AppSettingValue);
            _log.Info($"调用微信统一下单接口返回：{JsonConvert.SerializeObject(response.ReturnObj)}");
            return Ok(response);
        }


        [HttpPost]
        public void PayForOrdersNotify()
        {
            //获取请求参数
            var buffer = new MemoryStream();
            Request.Body.CopyToAsync(buffer);
            var xmlData = Encoding.UTF8.GetString(buffer.GetBuffer());

            if (!string.IsNullOrEmpty(xmlData))
            {
                var xml = new XmlDocument();
                xml.LoadXml(xmlData);
                //处理返回的值
                DataSet ds = new DataSet();
                StringReader stram = new StringReader(xmlData);
                XmlTextReader reader = new XmlTextReader(stram);
                ds.ReadXml(reader);
                string return_code = ds.Tables[0].Rows[0]["return_code"].ToString();
                if (return_code.ToUpper() == "SUCCESS")
                {
                    //通信成功  
                    string result_code = ds.Tables[0].Rows[0]["result_code"].ToString();//业务结果  
                    if (result_code.ToUpper() == "SUCCESS")
                    {
                        try
                        {
                            _log.Info($"付款成功:{xmlData}");
                            string orderCode = ds.Tables[0].Rows[0]["out_trade_no"].ToString();
                            //微信回调会一直调接口，判断一下status
                            var o = ordersService.GetOrderByOrderCodeAndStatus(orderCode, 1);
                            if (o != null)
                            {
                                o.Status = 2;
                                o.PayTime = DateTime.Now;
                                o.PayType = 1;
                                o.LastUpdateDate = DateTime.Now;
                                ordersService.UpdateOrderStatusByOrderCode(o);
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.Error(ex.ToString());
                        }
                    }
                    else
                    {
                        _log.Error("支付失败:" + xmlData);
                    }
                }
                else
                {
                    _log.Error("支付失败:" + xmlData);
                }
            }


        }
    }
}
