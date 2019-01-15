using Abp.AspNetCore.Mvc.Controllers;

namespace SimpleTaskApp.Web.Controllers
{
    public abstract class SimpleTaskAppControllerBase: AbpController
    {
        protected SimpleTaskAppControllerBase()
        {
            LocalizationSourceName = SimpleTaskAppConsts.LocalizationSourceName;
        }
    }
}