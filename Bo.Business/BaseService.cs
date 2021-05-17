using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Business
{
    public class BaseService : IBaseService
    {
        private IRepositoryFactory _repositoryFactory;
        private DBContext _dbContext;
        public BaseService(IRepositoryFactory repositoryFactory, DBContext _dbContext)
        {
            this._repositoryFactory = repositoryFactory;
            this._dbContext = _dbContext;
        }

        public IRepository<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_dbContext);
        }
    }
}
