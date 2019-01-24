using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using SimpleTaskApp.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Abp.PlugIns;
using System.IO;

namespace SimpleTaskApp.Web.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddAbpDbContext<SimpleTaskAppDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            #region ABP Module
            //services.AddAbp<MyStartupModule>(options =>
            //{
            //    options.PlugInSources.Add(new FolderPlugInSource(@"C:\MyPlugIns"));
            //});

            //services.AddAbp<MyStartupModule>(options =>
            //{
            //    options.PlugInSources.AddFolder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PlugIns"));
            //    var p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PlugIns");
            //    Console.WriteLine(p);
            //});
            #endregion

            //Configure Abp and Dependency Injection
            return services.AddAbp<SimpleTaskAppWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
