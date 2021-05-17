using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityResponse
{
    public class ResponseStore
    {
        public string Brand { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public List<string> StoreTagList { get; set; }
        public string StoreImageUrl { get; set; }
        public string Notice { get; set; }
        public string BackgroundImageUrl { get; set; }
    }
}
