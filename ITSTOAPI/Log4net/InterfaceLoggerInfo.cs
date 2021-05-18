using System;
using System.Collections.Generic;
using System.Text;

namespace ITSTOAPI.Log4net
{
    public class InterfaceLoggerInfo
    {
        public string Message { get; set; }
        public string Url { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public InterfaceLoggerInfo(string message, string url, string request, string response)
        {
            this.Message = message;
            this.Url = url;
            this.Request = request;
            this.Response = response;
        }
    }
}
