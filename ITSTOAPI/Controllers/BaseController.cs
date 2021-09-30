using Bo.Interface.IBusiness;
using Common.Tool;
using ITSTOAPI.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [TypeFilter(typeof(CustomAuthorization))]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
