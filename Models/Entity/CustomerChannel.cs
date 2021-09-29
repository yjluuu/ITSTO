using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class CustomerChannel
    {
        public long Id { get; set; }
        public string UserCode { get; set; }
        public string UnionId { get; set; }
        public string OpenId { get; set; }
        public string SessionKey { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
