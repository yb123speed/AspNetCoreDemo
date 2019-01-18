using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using SimpleTaskApp.Tasks.Dtos;

namespace SimpleTaskApp.Tasks
{
    public class TaskAppService : SimpleTaskAppAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;
         
        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ListResultDto<TaskListDto>> GetAllAsync(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t =>t.AssignedPerson)
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t=>t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(
                    ObjectMapper.Map<List<TaskListDto>>(tasks)
                );
        }
    }
}
