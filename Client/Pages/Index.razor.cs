using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Pages
{
    partial class Index : CustomComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            var test = await RepoApi.Customers.GetAllByRouteAsync();
        }
    }
}