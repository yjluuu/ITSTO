using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class Store
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string StoreTags { get; set; }
        public string StoreImageUrl { get; set; }
        public string Notice { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    
    }
}
