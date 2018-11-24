using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OidcSample.Data;
using Microsoft.EntityFrameworkCore;
using OidcSample.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4;
using OidcSample.Services;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace OidcSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=aspnet-IdentitySample-2E0D6D3E-3845-48CC-9797-7462F92983B7;Trusted_Connection=True;MultipleActiveResultSets=true";

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddInMemoryClients(Config.GetClients())
                //.AddInMemoryApiResources(Config.GetApiResouces())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddConfigurationStore(options=> {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, 
                            sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })              //Clients Scopes
                                //数据迁移  Add-Migration InitConfiguration -Context ConfigurationDbContext -OutputDir Data/Migrations/IdentityServer/ConfigurationDb
                                //DbContext 位于    IdentityServer4.EntityFramework.DbContexts 下
                .AddOperationalStore(options=> {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));
                    };
                })    //token grants
                      //数据迁移  Add-Migration InitPersistedGrant -Context PersistedGrantDbContext -OutputDir Data/Migrations/IdentityServer/PersistedGrantDb

                //.AddTestUsers(Config.GetTestUsers());
                .AddAspNetIdentity<ApplicationUser>()
                .Services.AddScoped<IProfileService, ProfileService>(); //依赖注入ProfileService

            #region old
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options=>{
            //        options.LoginPath = "/Account/Login";
            //        // options.LoginPath="";
            //        // options.LogoutPath="";
            //        // options.AccessDeniedPath="";//如果权限不够返回的页面
            //        // options.CookiePath="";//cookie的可用范围
            //    });

            //services.Configure<IdentityOptions>(options=> {
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            #endregion


            services.AddScoped<ConsentService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }

            this.InitIdentityServiceDataBase(app);
            app.UseStaticFiles();
            //app.UseAuthentication();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitIdentityServiceDataBase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                }

                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var api in Config.GetApiResouces())
                    {
                        configurationDbContext.ApiResources.Add(api.ToEntity());
                    }
                }

                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var identity in Config.GetIdentityResources())
                    {
                        configurationDbContext.IdentityResources.Add(identity.ToEntity());
                    }
                }

                configurationDbContext.SaveChanges();
            }
        }
    }
}
