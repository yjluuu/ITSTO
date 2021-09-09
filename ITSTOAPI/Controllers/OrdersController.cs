using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly ILog _log;
        private readonly IOrdersService ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            this._log = LogManager.GetLogger(typeof(OrdersController));
            this.ordersService = ordersService;
        }

        [HttpPost]
        public IActionResult NewOrders(Orders orders)
        {
            ApiBaseResponse response = new ApiBaseResponse();
            _log.Info($"新增订单入参：{JsonConvert.SerializeObject(orders)}");
            #region 验证参数
            if (string.IsNullOrEmpty(orders.Brand))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Brand不能为空");
                return Ok(response);
            }
            if (orders.OrderType == 0)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OrderType不能为空");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(orders.StoreCode))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "StoreCode不能为空");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(orders.UserCode))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "UserCode不能为空");
                return Ok(response);
            }
            if (orders.OriginalAmount == 0)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OriginalAmount不能为空");
                return Ok(response);
            }
            if (orders.ActualAmount == 0)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "ActualAmount不能为空");
                return Ok(response);
            }
            if (orders.OrderType == 0)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OrderType不能为空");
                return Ok(response);
            }
            if (orders.OrderDetailList == null)
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OrderDetailList不能为空");
                return Ok(response);
            }
            foreach (var item in orders.OrderDetailList)
            {
                if (string.IsNullOrEmpty(item.DishCode))
                {
                    response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "DishCode不能为空");
                    return Ok(response);
                }
                if (item.Amount == 0)
                {
                    response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Amount不能为空");
                    return Ok(response);
                }
                if (item.OriginalAmount == 0)
                {
                    response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "OriginalAmount不能为空");
                    return Ok(response);
                }
                if (item.ActualAmount == 0)
                {
                    response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "ActualAmount不能为空");
                    return Ok(response);
                }
            }
            #endregion
            //生成ordercode
            string orderCode = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random(System.Environment.TickCount).Next(100000, 999999);
            orders.OrderCode = orderCode;
            orders.CreateUser = "yjl";
            orders.CreateDate = DateTime.Now;
            orders.LastUpdateUser = "yjl";
            orders.LastUpdateDate = DateTime.Now;
            foreach (var item in orders.OrderDetailList)
            {
                item.OrderCode = orderCode;
                item.CreateUser = "yjl";
                item.CreateDate = DateTime.Now;
                item.LastUpdateUser = "yjl";
                item.LastUpdateDate = DateTime.Now;
            }
            bool insertResult = ordersService.NewOrders(orders);
            _log.Info($"新增订单执行结果：{JsonConvert.SerializeObject(insertResult)}");
            if (insertResult)
            {
                response.ReturnObj = new { OrderCode = orderCode };
                return Ok(response);
            }
            response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.SystemError, "新增订单失败");
            return Ok(response);
        }


    }
}
