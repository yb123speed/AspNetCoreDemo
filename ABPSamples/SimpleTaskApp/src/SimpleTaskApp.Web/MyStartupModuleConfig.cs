﻿using Abp.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTaskApp.Web
{
    public class MyStartupModuleConfig
    {
        public bool SampleConfig1 { get; set; }

        public string SampleConfig2 { get; set; }
    }

    public static class MyModuleConfigurationExtensions
    {
        public static MyStartupModuleConfig MyStarttupModule(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.AbpConfiguration.Get<MyStartupModuleConfig>();
        }
    }
}
