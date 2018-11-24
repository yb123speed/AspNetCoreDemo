using Microsoft.AspNetCore.Identity;
using OidcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace OidcSample.Data
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationUserRole> _roleManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider services)
        {
            if (!context.Roles.Any())
            {
                _roleManager = services.GetRequiredService<RoleManager<ApplicationUserRole>>();

                var role = new ApplicationUserRole() { Name= "Administrators",NormalizedName= "Administrators" };
                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception("初始化默认角色失败："+result.Errors.SelectMany(e=>e.Description));
                }


            }
            if (!context.Users.Any())
            {
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser() {
                    UserName="Administrator",
                    Email="904896209@qq.com",
                    NormalizedUserName="admin",
                    Avatar = "http://pic.qiantucdn.com/58pic/19/02/97/03m58PICDxF_1024.jpg"
                };

                var result = await _userManager.CreateAsync(defaultUser,"Password$123");
                await _userManager.AddToRoleAsync(defaultUser, "Administrators");

                if (!result.Succeeded)
                {
                    throw new Exception("初始默认用户失败");
                }
                    
             }
         
        }
    }
}
