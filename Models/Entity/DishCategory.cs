using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class DishCategory
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string StoreCode { get; set; }
        public string DishCategoryName { get; set; }
        public string DishCategoryCode { get; set; }
        public int Order { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
