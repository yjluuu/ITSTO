using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityResponse
{
    public class ResponseDishs
    {
        public string Brand { get; set; }
        public string StoreCode { get; set; }
        public string DishCategoryName { get; set; }
        public string DishCategoryCode { get; set; }
        public int Order { get; set; }
        public List<ResponseDish> DishList { get; set; }
    }

    public class ResponseDish
    {
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
    }
}
