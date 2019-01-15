using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SimpleTaskApp.Web.Startup;
namespace SimpleTaskApp.Web.Tests
{
    [DependsOn(
        typeof(SimpleTaskAppWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class SimpleTaskAppWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppWebTestModule).GetAssembly());
        }
    }
}