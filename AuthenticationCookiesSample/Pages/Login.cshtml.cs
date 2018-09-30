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
        //保存认证方案以及友好显示名称
        [BindProperty]
        public IDictionary<string, string> Schemes { get; set; } = new Dictionary<string, string>();

        [BindProperty]
        public string Scheme {get;set;}

        //认证后我们要跳转的页面
        [BindProperty]
        public string RedirectToUrl {get;set;}

        IAuthenticationSchemeProvider _authenticationSchemeProvider{get;set;}

        public LoginModel(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            //_authenticationSchemeProvider = (IAuthenticationSchemeProvider)HttpContext.RequestServices.GetService(typeof(IAuthenticationSchemeProvider));
            _authenticationSchemeProvider= authenticationSchemeProvider;
        }

        public async Task OnGetAsync(string ReturnUrl)
        {
            RedirectToUrl=ReturnUrl;
            var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
            foreach(var item in authenticationSchemes)
            {
                Schemes.Add(item.Name,item.DisplayName??item.Name);
            }
            if(ReturnUrl.Contains("Admin"))
            {
                Scheme="Admin";
            }
            else
            {
                Scheme="Cookies";
            }

        }

        public IActionResult OnPost(string scheme, string redirectToUrl)
        {
            //登陆验证

            //保存cookie
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,scheme)
            };

            var claimsIdentity = new ClaimsIdentity(claims, scheme);
            HttpContext.SignInAsync(scheme, new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect(redirectToUrl);
        }
    }
}
