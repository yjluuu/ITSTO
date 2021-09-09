using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using log4net;
using Routine.Models.ApiEntityRequest;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Business
{
    public class DishCategoryService : BaseService, IDishCategoryService
    {
        private readonly ILog _log;
        private IRepository<DishCategory> dishCategoryService;
        public DishCategoryService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(DishCategoryService));
            this.dishCategoryService = this.CreateService<DishCategory>();
        }

        public IQueryable<DishCategory> GetDishCategorysByStoreCode(RequestDishCategory requestDishCategory)
        {
            return dishCategoryService.Where(d => !d.IsDeleted && d.Brand == requestDishCategory.Brand && d.StoreCode == requestDishCategory.StoreCode);
        }
    }
}
