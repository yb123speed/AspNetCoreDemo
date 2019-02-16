using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SimpleTaskApp.Configuration;
using SimpleTaskApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SimpleTaskApp.Dapper;

namespace SimpleTaskApp.Web.Startup
{
    [DependsOn(
        typeof(SimpleTaskAppApplicationModule), 
        typeof(SimpleTaskAppEntityFrameworkCoreModule), 
        typeof(SimpleTaskAppDapperModule),
        typeof(AbpAspNetCoreModule))]
    public class SimpleTaskAppWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SimpleTaskAppWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SimpleTaskAppConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SimpleTaskAppNavigationProvider>();

            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SimpleTaskAppApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppWebModule).GetAssembly());
        }
    }
}