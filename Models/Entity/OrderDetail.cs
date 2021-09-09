using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string DishCode { get; set; }
        public int Amount { get; set; }
        public double OriginalAmount { get; set; }
        public double ActualAmount { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
