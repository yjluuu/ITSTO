using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
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
        public DishCategoryService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }

        public IQueryable<DishCategory> GetDishCategorysByStoreCode(RequestDishCategory requestDishCategory)
        {
            var dishCategoryService = this.CreateService<DishCategory>();
            return dishCategoryService.Where(d => !d.IsDeleted && d.Brand == requestDishCategory.Brand && d.StoreCode == requestDishCategory.StoreCode);
        }
    }
}
