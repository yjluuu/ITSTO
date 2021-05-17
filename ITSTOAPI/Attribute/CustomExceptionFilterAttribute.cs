using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Attribute
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //private readonly ILogger _log = new Log4NetProvider().CreateLogger();
        private readonly ILogger<CustomExceptionFilterAttribute> _log;
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> _log)
        {
            this._log = _log;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                _log.LogError($"EXCEPTION：{context.Exception.Message}。");
                context.ExceptionHandled = true;
            }
        }
    }
}
