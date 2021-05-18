using Bo.Interface.IBusiness;
using Common.Tool;
using ITSTOAPI.Log4net;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ITSTOAPI.Attribute
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        //private readonly ILogger _log = new Log4NetProvider().CreateLogger();
        private readonly ILog _log;
        public CustomActionFilterAttribute()
        {
            this._log = LogManager.GetLogger(typeof(CustomActionFilterAttribute));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //log一下进入方法的入参
            _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Request:{JsonConvert.SerializeObject(context.ActionArguments.Values)}。");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            string wholePath = context.HttpContext.Request.Path.ToString();
            int secondSlash = wholePath.IndexOf("/", wholePath.IndexOf("/") + 1 + 1);
            string path = wholePath.Substring(secondSlash);
            _log.Info(new InterfaceLoggerInfo(context.Result.ToString(), path, context.HttpContext.Request.ToString(), context.HttpContext.Response.ToString()));
            _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Response:{JsonConvert.SerializeObject(context.Result)}。");
        }
    }
}
