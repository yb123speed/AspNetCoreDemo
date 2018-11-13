using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidationSample.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace FluentValidationSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("hobbies1")]
        public IActionResult GetHobbies1([FromQuery] QueryStudentHobbiesDto dto)
        {
            var validator = new QueryStudentHobbiesDtoValidator();
            var results = validator.Validate(dto, ruleSet: "all");
            return !results.IsValid
               ? Ok(new { code = -1, data = new List<string>(), msg = results.Errors.FirstOrDefault().ErrorMessage })
               : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

        private readonly IValidator<QueryStudentHobbiesDto> _validator;
        private readonly IStudentService _studentService;
        public ValuesController(IValidator<QueryStudentHobbiesDto> validator, IStudentService studentService)
        {
            _validator = validator;
            _studentService = studentService;
        }

        [HttpGet("hobbies2")]
        public IActionResult GetHobbies2([FromQuery] QueryStudentHobbiesDto dto)
        {
            var results = _validator.Validate(dto, ruleSet: "all");
            return !results.IsValid
               ? Ok(new { code = -1, data = new List<string>(), msg = results.Errors.FirstOrDefault().ErrorMessage })
               : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }

        // GET api/values/hobbies3  
        [HttpGet("hobbies3")]
        public ActionResult GetHobbies3([FromQuery]QueryStudentHobbiesDto dto)
        {
            var (flag, msg) = _studentService.QueryHobbies(dto);

            return !flag
                ? Ok(new { code = -1, data = new List<string>(), msg })
                : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }
    }
}
