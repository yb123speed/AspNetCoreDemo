using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace SimpleTaskApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class SimpleTaskAppEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppEntityFrameworkCoreModule).GetAssembly());
        }
    }
}