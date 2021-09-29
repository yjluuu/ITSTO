using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityRequest
{
    public class RequestPayForOrders
    {
        public string Brand { get; set; }
        public string OrderCode { get; set; }
        public string UserCode { get; set; }
    }
}
