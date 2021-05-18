using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSTOAPI.Log4net
{
    public class InterfaceLoggerConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
            var interfaceLoggerInfo = loggingEvent.MessageObject as InterfaceLoggerInfo;
            if (interfaceLoggerInfo == null)
            {
                writer.Write("");
            }
            else
            {
                switch (this.Option.ToLower())
                {
                    case "message":
                        writer.Write(interfaceLoggerInfo.Message);
                        break;
                    case "url":
                        writer.Write(interfaceLoggerInfo.Url);
                        break;
                    case "request":
                        writer.Write(interfaceLoggerInfo.Request);
                        break;
                    case "response":
                        writer.Write(interfaceLoggerInfo.Response);
                        break;
                    default:
                        writer.Write("");
                        break;
                }
            }
        }
    }
}
