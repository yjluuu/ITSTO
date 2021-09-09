using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IChannelService
    {
        Channel GetChannelByBrand(string brand);
    }
}
