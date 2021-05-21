using AutoMapper;
using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Routine.Models.ApiEntityRequest;
using Routine.Models.ApiEntityResponse;
using Routine.Models.EnmuEntity;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{

    public class DishController : BaseController
    {
        private readonly ILog _log;
        private readonly IDishCategoryService _dishCategoryService;
        private readonly IDishService _dishService;
        private readonly IMapper mapper;
        public DishController(IDishCategoryService _dishCategoryService, IDishService _dishService, IMapper mapper)
        {
            this._log = LogManager.GetLogger(typeof(DishController));
            this._dishCategoryService = _dishCategoryService;
            this._dishService = _dishService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult GetDishs(RequestDishCategory requestDishCategory)
        {
            ApiBaseResponse response = new ApiBaseResponse();
            _log.Info($"获取菜品分类信息入参：{JsonConvert.SerializeObject(requestDishCategory)}");
            if (string.IsNullOrEmpty(requestDishCategory.Brand))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "Brand不能为空");
                return Ok(response);
            }
            if (string.IsNullOrEmpty(requestDishCategory.StoreCode))
            {
                response.GetErrorApiBaseResponse(ApiBaseResponseStatusCodeEnum.NoDetailedInfo, "StoreCode不能为空");
                return Ok(response);
            }
            var dishCategorys = _dishCategoryService.GetDishCategorysByStoreCode(requestDishCategory).ToList();
            _log.Info($"获取菜品分类信息查询结果：{JsonConvert.SerializeObject(dishCategorys)}");
            var dishs = _dishService.GetAllDishs(new Dish { Brand = requestDishCategory.Brand }).ToList();
            _log.Info($"获取菜品详情信息查询结果：{JsonConvert.SerializeObject(dishs)}");
            var responseDishCategory = mapper.Map<List<ResponseDishCategory>>(dishCategorys);
            var responseDishList = mapper.Map<List<ResponseDish>>(dishs);
            foreach (var item in responseDishCategory)
            {
                item.DishIds = responseDishList.Where(d => d.DishCategoryCode == item.DishCategoryCode).Select(d => d.Id).ToList();
            }
            Dictionary<int, ResponseDish> dic = new Dictionary<int, ResponseDish>();
            foreach (var item in responseDishList)
            {
                dic.Add(item.Id, item);
            }
            response.ReturnObj = JsonConvert.SerializeObject(new ResponseDishCompose() { DishCategorys = responseDishCategory, Dishs = dic });
            _log.Info($"获取菜品信息返回结果：{JsonConvert.SerializeObject(response.ReturnObj)}");
            return Ok(response);
        }
    }
}
