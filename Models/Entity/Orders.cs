using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Routine.Models.Entity
{
    public class Orders
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string OrderCode { get; set; }
        public int OrderType { get; set; }
        public string StoreCode { get; set; }
        public string UserCode { get; set; }
        public double OriginalAmount { get; set; }
        public double ActualAmount { get; set; }
        public string Scene { get; set; }
        public int Status { get; set; }
        public DateTime? PayTime { get; set; }
        public int? PayType { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public List<OrderDetail> OrderDetailList { get; set; }
    }
}
