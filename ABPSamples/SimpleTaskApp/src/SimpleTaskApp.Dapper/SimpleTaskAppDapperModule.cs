using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleTaskApp.Dapper
{
    [DependsOn(
        typeof(SimpleTaskAppCoreModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpDapperModule))]
    public class SimpleTaskAppDapperModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleTaskAppDapperModule).GetAssembly());

            //这里会自动去扫描程序集中配置好的映射关系
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(SimpleTaskAppDapperModule).GetAssembly() });
        }
    }
}
