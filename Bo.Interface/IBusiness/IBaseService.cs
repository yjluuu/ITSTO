using Bo.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IBaseService
    {
        IRepository<T> CreateService<T>() where T : class, new();
    }
}
