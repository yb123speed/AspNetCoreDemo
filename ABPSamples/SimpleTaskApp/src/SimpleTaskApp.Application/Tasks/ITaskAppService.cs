using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimpleTaskApp.Tasks.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskApp.Tasks
{
    public interface ITaskAppService:IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAllAsync(GetAllTasksInput input);

    }
}
