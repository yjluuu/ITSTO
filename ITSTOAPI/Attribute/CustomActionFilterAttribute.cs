using Bo.Interface.IBusiness;
using Common.Tool;
using ITSTOAPI.Log4net;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITSTOAPI.Attribute
{
    public class CustomActionFilterAttribute : IActionFilter//ActionFilterAttribute
    {
        //private readonly ILogger _log = new Log4NetProvider().CreateLogger();
        private readonly ILog _log;
        private readonly IInterfaceLogsService interfaceLogsService;
        public CustomActionFilterAttribute(IInterfaceLogsService interfaceLogsService)
        {
            this._log = LogManager.GetLogger(typeof(CustomActionFilterAttribute));
            this.interfaceLogsService = interfaceLogsService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string wholePath = context.HttpContext.Request.Path.ToString();
            int secondSlash = wholePath.IndexOf("/", wholePath.IndexOf("/") + 1 + 1);
            string path = wholePath.Substring(secondSlash);

            string requestContext = string.Empty;
            using (var reader = new StreamReader(context.HttpContext.Request.Body, System.Text.Encoding.UTF8))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);  //大概是== Request.Body.Position = 0;的意思
                requestContext = reader.ReadToEndAsync().Result.Replace("\r\n", "").Replace(" ", "");
                reader.BaseStream.Seek(0, SeekOrigin.Begin);  //读完后也复原
            }
            //log4配置了往表里写日志，但是写不进去，不知道啥问题
            //_log.Info(new InterfaceLoggerInfo(string.Empty, path, requestContext, JsonConvert.SerializeObject(context.Result)));
            //这样记日志回影响代码逻辑，代码里不savechanges时到这里总会savechanges
            //interfaceLogsService.LogInterface(new InterfaceLogs { LogThread = Thread.CurrentThread.ManagedThreadId.ToString(), LogLevel = "Info", Url = path, Request = requestContext, Response = JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).Value), LogDate = DateTime.Now });
            _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Response:{JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).Value)}。");

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //log一下进入方法的入参
            _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Request:{JsonConvert.SerializeObject(context.ActionArguments)}。");
        }

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    //log一下进入方法的入参
        //    _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Request:{JsonConvert.SerializeObject(context.ActionArguments.Values)}。");
        //}

        //public override void OnResultExecuted(ResultExecutedContext context)
        //{
        //    string wholePath = context.HttpContext.Request.Path.ToString();
        //    int secondSlash = wholePath.IndexOf("/", wholePath.IndexOf("/") + 1 + 1);
        //    string path = wholePath.Substring(secondSlash);

        //    //log4配置了往表里写日志，但是写不进去，不知道啥问题
        //    //_log.Info(new InterfaceLoggerInfo(context.Result.ToString(), path, context.HttpContext.Request.ToString(), context.HttpContext.Response.ToString()));
        //    interfaceLogsService.LogInterface(new InterfaceLogs { LogThread = Thread.CurrentThread.ManagedThreadId.ToString(), LogLevel = "Info", Url = path, Request = JsonConvert.SerializeObject(context.Result), Response = JsonConvert.SerializeObject(context.Result), LogDate = DateTime.Now });
        //    _log.Info($@"$$$》Controller：{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName}，Action:{((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName}，Response:{JsonConvert.SerializeObject(context.Result)}。");
        //}
    }
}
