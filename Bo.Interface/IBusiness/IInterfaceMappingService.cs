using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IInterfaceMappingService
    {
        IQueryable<InterfaceMapping> GetInterfaceMappingByUser(InterfaceMapping interfaceMapping);
    }
}
