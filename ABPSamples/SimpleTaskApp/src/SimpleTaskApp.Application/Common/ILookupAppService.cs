using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskApp.Common
{
    public interface ILookupAppService:IApplicationService
    {
        Task<ListResultDto<ComboboxItemDto>> GetPeopleComboboxItemsAsync();
    }
}
