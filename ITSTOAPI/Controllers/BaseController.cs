using ITSTOAPI.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSTOAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[TypeFilter(typeof(CustomActionFilterAttribute))]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
