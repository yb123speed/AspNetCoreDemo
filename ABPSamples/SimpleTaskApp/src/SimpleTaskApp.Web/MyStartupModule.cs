using Abp.Configuration.Startup;
using Abp.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleTaskApp.Web
{
    public class MyStartupModule:AbpModule
    {
        public MyStartupModule()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PreInitialize()
        {
            //将AbpWebCommon模块配置为向客户端发送所有异常。
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

            //注册模块配置类，它应该像这样注册为Singleton。
            IocManager.Register<MyStartupModuleConfig>();

            Configuration.Get<MyStartupModuleConfig>().SampleConfig1 = false;
        }
    }
}
