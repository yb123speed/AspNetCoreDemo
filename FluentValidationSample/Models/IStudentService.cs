using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationSample.Models
{
    public interface IStudentService
    {
        (bool flag, string msg) QueryHobbies(QueryStudentHobbiesDto dto);
    }
}
