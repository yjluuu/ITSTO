using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class Dish
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string DishCategoryCode { get; set; }
        public string DishName { get; set; }
        public string DishCode { get; set; }
        public string DishImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int MonthlySales { get; set; }
        //1：在售  2：售罄 3：下架
        public int Status { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
