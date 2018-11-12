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
        [HttpGet]
        public IActionResult GetHobbies1([FromBody] QueryStudentHobbiesDto dto)
        {
            var validator = new QueryStudentHobbiesDtoValidator();
            var results = validator.Validate(dto, ruleSet: "all");
            return !results.IsValid
               ? Ok(new { code = -1, data = new List<string>(), msg = results.Errors.FirstOrDefault().ErrorMessage })
               : Ok(new { code = 0, data = new List<string> { "v1", "v2" }, msg = "" });
        }
    }
}
