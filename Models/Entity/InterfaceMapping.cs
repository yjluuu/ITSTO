using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class InterfaceMapping
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int InterfaceUserId { get; set; }
        public string InterfacePath { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
