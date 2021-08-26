using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    public class WeChatController : BaseController
    {
        private readonly ILog _log;
        private readonly IHttpContextAccessor accessor;
        private readonly IAppSettingService appSettingService;
        public WeChatController(IHttpContextAccessor accessor, IAppSettingService appSettingService)
        {
            this.accessor = accessor;
            this._log = LogManager.GetLogger(typeof(StoreController));
            this.appSettingService = appSettingService;
        }
    }
}
