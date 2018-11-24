using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using OidcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OidcSample.Services
{
    public class ConsentService
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resoureStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public ConsentService(
            IClientStore clientStore,
            IResourceStore resourceStore,
            IIdentityServerInteractionService identityServerInteractionService
            )
        {
            _clientStore = clientStore;
            _resoureStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }

        #region Private Methods

        /// <summary>
        /// 创建ConsentViewModel
        /// </summary>
        /// <param name="client"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        private ConsentViewModel CreateConsentViewModel(Client client, Resources resources,InputConsentViewModel model=null)
        {
            var rememberConsent = model?.RememberConsent ?? true;
            var selectedScopes = model?.ScopesConsented??Enumerable.Empty<string>();


            var vm = new ConsentViewModel();
            vm.ClientName = client.ClientName;
            vm.ClientUrl = client.ClientUri;
            vm.ClientLogoUrl = client.LogoUri;
            vm.RememberConsent = rememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i,selectedScopes.Contains(i.Name)||model==null));
            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(x => CreateScopeViewModel(x, selectedScopes.Contains(x.Name) || model == null));
            return vm;
        }

        /// <summary>
        /// 创建IdentityResource的ViewModel
        /// </summary>
        /// <param name="identityResource"></param>
        /// <returns></returns>
        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource,bool check)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Required = identityResource.Required,
                Checked = check || identityResource.Required,
                Emphasize = identityResource.Emphasize
            };
        }

        /// <summary>
        /// 创建ApiResource的ViewModel
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Required = scope.Required,
                Checked = check || scope.Required,
                Emphasize = scope.Emphasize
            };
        }

        #endregion

        public async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl,InputConsentViewModel model=null)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
            {
                return null;
            }

            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);

            var resource = await _resoureStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

            var vm = CreateConsentViewModel(client, resource, model);
            vm.ReturnUrl = returnUrl;

            return vm;
        }

        public async Task<ProcessConsentResult> ProcessConsent(InputConsentViewModel model)
        {
            ConsentResponse consentResponse = null;
            var result = new ProcessConsentResult();
            if (model.Button == "no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else if (model.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = model.ScopesConsented,
                    };
                }
                else
                {
                    result.ValidationError = "请至少选中一个权限";
                }
                
            }

            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);

                result.ReturnUrl = model.ReturnUrl;
            }
            else
            {
                var consentViewModel = await BuildConsentViewModel(model.ReturnUrl);
                result.ViewModel = consentViewModel;
            }

            return result;
        }
    }
}
