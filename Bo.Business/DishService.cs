using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using log4net;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Business
{
    public class DishService : BaseService, IDishService
    {
        private readonly ILog _log;
        private readonly IRepository<Dish> dishService;
        public DishService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
            this._log = LogManager.GetLogger(typeof(DishService));
            this.dishService = this.CreateService<Dish>(); ;
        }

        public IQueryable<Dish> GetAllDishs(Dish dish)
        {
            return dishService.Where(d => !d.IsDeleted && d.Brand == dish.Brand);
        }

        public Dish GetDishByDishCode(string dishCode)
        {
            return dishService.FirstOrDefault(d => !d.IsDeleted && d.DishCode == dishCode);
        }
    }
}
