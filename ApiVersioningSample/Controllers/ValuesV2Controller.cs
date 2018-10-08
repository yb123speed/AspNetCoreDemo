using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningSample.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/values")]
    [ApiController]
    public class ValuesV2Controller : ControllerBase
    {
        // GET api/values
        //https://localhost:5001/api/values?api-version=2.0
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1 from Version 2", "value2 from Version 2" };
        }
    }
}
