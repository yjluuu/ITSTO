using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityResponse
{
    public class ResponseOrderDetail
    {
        public string OrderCode { get; set; }
        public string DishCode { get; set; }
        public string DishName { get; set; }
        public int Amount { get; set; }
        public double OriginalAmount { get; set; }
        public double ActualAmount { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
