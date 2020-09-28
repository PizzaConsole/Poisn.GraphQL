using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Poisn.GraphQL.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Pages
{
    public class CustomComponentBase : ComponentBase
    {
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected HttpClient HttpClient { get; set; }

        [Inject]
        protected IServiceProvider ServiceProvider { get; set; }

        [Inject]
        protected IRepoServiceApi RepoApi { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IAuthorizationService AuthorizationService { get; set; }
    }
}