using Abp.AspNetCore.Mvc.Views;

namespace SimpleTaskApp.Web.Views
{
    public abstract class SimpleTaskAppRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected SimpleTaskAppRazorPage()
        {
            LocalizationSourceName = SimpleTaskAppConsts.LocalizationSourceName;
        }
    }
}
