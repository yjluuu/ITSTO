using AutoMapper;
using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Routine.Models.ApiEntityRequest;
using Routine.Models.ApiEntityResponse;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{

    public class StoreController : BaseController
    {
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly ILogger<StoreController> log;
        public StoreController(IStoreService storeService, IMapper mapper, ILogger<StoreController> log)
        {
            this.storeService = storeService;
            this.mapper = mapper;
            this.log = log;
        }

        [HttpPost]
        public IActionResult GetStores(RequestStore requestStore)
        {
            log.LogInformation($"获取门店信息入参：{JsonConvert.SerializeObject(requestStore)}");
            Store store = storeService.GetSores(requestStore);
            log.LogInformation($"获取门店信息查询结果：{JsonConvert.SerializeObject(store)}");
            List<string> storeTagList = store.StoreTags.Split(',').ToList();
            var returnStore = mapper.Map<ResponseStore>(store);
            returnStore.StoreTagList = storeTagList;
            log.LogInformation($"获取门店信息返回结果：{JsonConvert.SerializeObject(returnStore)}");
            return Ok(returnStore);
        }
    }
}
