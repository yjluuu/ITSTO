using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class Customer
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string UserCode { get; set; }
        public string Mobile { get; set; }
        public string NickName { get; set; }
        public string HeadImageUrl { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
