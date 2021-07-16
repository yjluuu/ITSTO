using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class InterfaceLogs
    {
        public long Id { get; set; }
        public string LogThread { get; set; }
        public string LogLevel { get; set; }
        public string LoggerName { get; set; }
        public string Url { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string LogMessage { get; set; }
        public DateTime LogDate { get; set; }
    }
}
