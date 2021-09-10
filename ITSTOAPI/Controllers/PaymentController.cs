using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Routine.Models.ApiEntityRequest;
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
        private readonly static string appid; // 这里填写你自己的appid
        private readonly static string mch_id; // 这里填写你自己的商户号
        private readonly static string partnerkey; // 这里填写你自己的商户秘钥
        private readonly static string notifyurl;// 回调地址随便外网能访问就行
        public readonly static string url;// 统一下单接口
        public readonly static string wxUrl; // 获取退款的api接口

        private readonly ILog _log;
        private readonly IAppSettingService appSettingService;
        private readonly IChannelService channelService;
        public PaymentController(IAppSettingService appSettingService,
                                 IChannelService channelService)
        {
            _log = LogManager.GetLogger(typeof(DishController));
            this.appSettingService = appSettingService;
            this.channelService = channelService;
        }

        public IActionResult PayForOrders(RequestPayForOrders request)
        {

            return null;
        }


    }
}
