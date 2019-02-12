using Abp.Dapper;
using Abp.Modules;
using System;

namespace SimpleTaskApp.Dapper
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule),
        typeof(AbpDapperModule))]
    public class SimpleTaskAppDapperModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppDapperModule).GetAssembly());
        }
    }
}
