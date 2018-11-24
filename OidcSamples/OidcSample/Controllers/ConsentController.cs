using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OidcSample.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using OidcSample.Services;

namespace OidcSample.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ConsentService _consentService;

        public ConsentController(ConsentService consentService)
        {
            _consentService = consentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model =await _consentService.BuildConsentViewModel(returnUrl);

            if (model == null)
            {

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel viewModel)
        {
            var result = await _consentService.ProcessConsent(viewModel);
            if (result.IsRedirct)
            {
                return Redirect(result.ReturnUrl);
            }

            if (!string.IsNullOrWhiteSpace(result.ValidationError))
            {
                ModelState.AddModelError("",result.ValidationError);
            }
            return View(result.ViewModel);
        }

    }
}