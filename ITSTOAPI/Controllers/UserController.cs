using AutoMapper;
using Bo.Interface.IBusiness;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ITestService testService;
        //private readonly IMapper mapper;
        //public UserController(ITestService testService, IMapper mapper)
        //{
        //    this.testService = testService;
        //    this.mapper = mapper;
        //}

        [HttpGet]
        public IActionResult Login()
        {
            //var ssss = testService.GetTests();
            //testService.Insert();
            return Ok("hello");
        }

    }
}
