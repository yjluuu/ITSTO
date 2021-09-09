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
using log4net;

namespace Bo.Business
{
    public class StoreService : BaseService, IStoreService
    {
        private readonly ILog _log;
        private readonly IRepository<Store> storeService;
        public StoreService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(StoreService));
            this.storeService = this.CreateService<Store>();
        }

        public Store GetSores(RequestStore requestStore)
        {
            return storeService.Where(t => t.Brand == requestStore.Brand && !t.IsDeleted).FirstOrDefault();
        }
    }
}
