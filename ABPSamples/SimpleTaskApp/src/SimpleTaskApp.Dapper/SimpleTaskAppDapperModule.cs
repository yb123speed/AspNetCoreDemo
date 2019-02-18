using Abp.Dapper;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DapperExtensions.Sql;
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

            //Fix the Bug (https://github.com/tmsmith/Dapper-Extensions/issues/145  https://github.com/aspnetboilerplate/aspnetboilerplate/issues/3987)
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();

            //这里会自动去扫描程序集中配置好的映射关系
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(SimpleTaskAppDapperModule).GetAssembly() });
        }
    }
}
