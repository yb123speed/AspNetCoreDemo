using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleTaskApp.Common;
using SimpleTaskApp.Tasks;
using SimpleTaskApp.Tasks.Dtos;
using SimpleTaskApp.Web.Models.People;
using SimpleTaskApp.Web.Models.Tasks;

namespace SimpleTaskApp.Web.Controllers
{
    public class TasksController : SimpleTaskAppControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ILookupAppService _lookupAppService;

        public TasksController(
            ITaskAppService taskAppService,
            ILookupAppService  lookupAppService)
        {
            _taskAppService = taskAppService;
            _lookupAppService = lookupAppService;
        }

        public async Task<IActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAllAsync(input);
            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskState = input.State
            };
            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var peopleSelectListItems = (await _lookupAppService.GetPeopleComboboxItemsAsync()).Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            peopleSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            return View(new CreateTaskViewModel(peopleSelectListItems));
        }
    }
}