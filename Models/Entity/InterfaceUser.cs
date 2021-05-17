using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class InterfaceUser
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
