using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Routine.Models.ApiEntityRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly ILog _log;
        private readonly IAppSettingService appSettingService;
        public PaymentController(IAppSettingService appSettingService)
        {
            _log = LogManager.GetLogger(typeof(DishController));
            this.appSettingService = appSettingService;
        }

        public IActionResult PayForOrders(RequestPayForOrders request)
        {

            return null;
        }
    }
}
