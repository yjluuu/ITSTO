using Bo.Interface.IBusiness;
using Common.Tool;
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
        private readonly ILogger<CustomActionFilterAttribute> _log;
        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> _log)
        {
            this._log = _log;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //log一下进入方法的入参
            _log.LogInformation($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Request:{JsonConvert.SerializeObject(context.ActionArguments.Values)}。");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _log.LogInformation($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Exception:{JsonConvert.SerializeObject(context.Exception)}。");
            _log.LogInformation($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Response:{JsonConvert.SerializeObject(context.Result)}。");
        }
    }
}
