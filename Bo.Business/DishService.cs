using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Business
{
    public class DishService : BaseService, IDishService
    {
        public DishService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext) { }

        public IQueryable<Dish> GetAllDishs(Dish dish)
        {
            var dishService = this.CreateService<Dish>();
            return dishService.Where(d => !d.IsDeleted && d.Brand == dish.Brand);
        }
    }
}
