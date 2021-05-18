using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Routine.Models.ApiEntityRequest;
using System.Web.Http;
using Common.Tool;

namespace Bo.Business
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }

        public Store GetSores(RequestStore requestStore)
        {
            var storeService = this.CreateService<Store>();
            return storeService.Where(t => t.Brand == requestStore.Brand && t.IsDeleted == false).FirstOrDefault();
        }
    }
}
