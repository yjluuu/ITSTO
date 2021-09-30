using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface IDishService
    {
        IQueryable<Dish> GetAllDishs(Dish dish);
        Dish GetDishByDishCode(string dishCode);
    }
}
