using Bo.Interface.IBusiness;
using Common.Tool;
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
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace ITSTOAPI.Controllers
{
    public class PaymentController : BaseController
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
                total_fee = (int)order?.ActualAmount * 100,
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
            var result = WXOrderPayForHelper.Getprepay(payParam, Configuration["PartnerKey"], appSettingService.GetAppSettingByKey(request.Brand, "WeChatPayForUrl")?.AppSettingValue);
            return Ok(result);
        }

        public int Test()
        {
            return 1;
        }
    }
}
