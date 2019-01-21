using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTaskApp.Web.Models.People
{
    public class CreateTaskViewModel
    {
        public List<SelectListItem> People { get; set; }

        public CreateTaskViewModel(List<SelectListItem> people)
        {
            People = people;
        }
    }
}
