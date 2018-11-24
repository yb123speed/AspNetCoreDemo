using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OidcSample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OidcSample.ViewModels;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Test;
using IdentityServer4.Services;

namespace OidcSample.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IIdentityServerInteractionService _interaction;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }

        //private readonly TestUserStore _users;

        //public AccountController(TestUserStore users)
        //{
        //    _users = users;
        //}


       

        public IActionResult Register(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            #region old
            ViewData["ReturnUrl"] = returnUrl;
            var identityUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
            };
            var identityResult = await _userManager.CreateAsync(identityUser, model.Password);

            if (identityResult.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });
                return RedirectToLocal(returnUrl);
            }
            #endregion

            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email not exist!");
            }
            else {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthenticationProperties props = null;
                    if (model.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };
                    }
                    //HttpContext.SignInAsync();
                    //与HttpContext.SignInAsync();冲突 强势使用扩展方法
                    //await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                    //    HttpContext,
                    //    user.SubjectId,
                    //    user.Username,
                    //    props
                    //);

                    await _signInManager.SignInAsync(user, props);

                    if (_interaction.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl); 
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError(nameof(model.Password), "Wrong Password");
            }

            return View();
        }

        //public IActionResult MakeLogin()
        //{
        //    //创建Cookie
        //    var claims=new List<Claim>{
        //        new Claim(ClaimTypes.Name,"yebin"),
        //        new Claim(ClaimTypes.Role,"admin")
        //    };

        //    var claimIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimIdentity));
        //    return Ok();
        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
