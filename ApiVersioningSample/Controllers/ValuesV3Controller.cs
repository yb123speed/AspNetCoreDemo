using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningSample.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/{v:apiVersion}/values")]
    [ApiController]
    public class ValuesV3Controller : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1 from Version 3", "value2 from Version 3" };
        }
    }
}
