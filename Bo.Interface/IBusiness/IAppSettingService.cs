using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IAppSettingService
    {
        AppSetting GetAppSettingByKey(string brand, string appSettingKey);
    }
}
