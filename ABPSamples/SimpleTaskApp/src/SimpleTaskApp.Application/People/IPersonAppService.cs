using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTaskApp.People
{
    public interface IPersonAppService:IApplicationService
    {
        void DoSomeStuff();
    }
}
