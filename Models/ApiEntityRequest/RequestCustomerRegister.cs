using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.ApiEntityRequest
{
    public class RequestCustomerRegister
    {
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
        public string UnionId { get; set; }
        public string OpenId { get; set; }
        public string SessionKey { get; set; }

        //登录时获取的 code ,用于生成openid
        public string JsCode { get; set; }
    }
}
