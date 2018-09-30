using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationCookiesSample.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            //登陆验证
            
            //保存cookie
            var claims =new List<Claim>(){
                new Claim(ClaimTypes.Name,"Yebin")
            };

            var claimsIdentity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();
            HttpContext.Response.Redirect("/");
        }
    }
}
