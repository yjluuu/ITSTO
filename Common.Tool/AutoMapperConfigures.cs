using AutoMapper;
using Routine.Models.ApiEntityResponse;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Tool
{
    public class AutoMapperConfigures : Profile
    {
        public AutoMapperConfigures()
        {
            CreateMap<Store, ResponseStore>();
            CreateMap<DishCategory, ResponseDishCategory>();
            CreateMap<Dish, ResponseDish>();
            CreateMap<Orders, ResponseOrders>();
            CreateMap<OrderDetail, ResponseOrderDetail>();
        }
    }
}
