using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> CreateRepository<T>(DBContext _dbContext) where T : class
        {
            return new Repository<T>(_dbContext);
        }
    }
}
