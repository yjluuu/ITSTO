using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IRepository
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(DBContext _dbContext) where T : class;
    }
}
