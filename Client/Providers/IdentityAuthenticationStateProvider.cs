using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Poisn.GraphQL.Shared.Entities;

namespace Poisn.GraphQL.Client.Providers
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityAuthenticationStateProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // get HttpClient lazily from IServiceProvider as you cannot use standard dependency
            // injection due to the AuthenticationStateProvider being initialized prior to
            // NavigationManager ( https://github.com/aspnet/AspNetCore/issues/11867 )
            var http = _serviceProvider.GetRequiredService<HttpClient>();
            string apiurl = "api/Users/authenticate";
            User user = await http.GetFromJsonAsync<User>(apiurl);

            ClaimsIdentity identity = new ClaimsIdentity();
            if (user.IsAuthenticated)
            {
                identity = new ClaimsIdentity("Identity.Application");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void NotifyAuthenticationChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}