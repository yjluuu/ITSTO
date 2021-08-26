using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string AppSettingKey { get; set; }
        public string AppSettingValue { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
