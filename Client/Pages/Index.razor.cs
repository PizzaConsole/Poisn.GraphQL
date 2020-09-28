using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Pages
{
    partial class Index : CustomComponentBase
    {
        private string _graphQlJson = "";

        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(NavigationManager.BaseUri + "graphql");

            var graphQLClient = new GraphQLHttpClient(uri, new SystemTextJsonSerializer());

            var heroRequest = new GraphQLRequest
            {
                Query = @"mutation($credentials:LoginInput!){
  login(credentials: $credentials){
    id
    username
  }
}",
                Variables = new
                {
                    credentials = new
                    {
                        username = "test",
                        password = "test"
                    }
                }
            };

            var graphQLResponse = await graphQLClient.SendMutationAsync(heroRequest, () => new { userLogin = new User() });

            var personName = graphQLResponse.Data.userLogin;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            _graphQlJson = JsonSerializer.Serialize(personName, options);
        }
    }
}