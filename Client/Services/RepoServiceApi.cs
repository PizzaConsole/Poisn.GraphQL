using Poisn.GraphQL.Shared.Entities;
using Poisn.GraphQL.Shared.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public class RepoServiceApi : IRepoServiceApi, IService
    {
        public IRepoService<Customer> Customers { get; private set; }
        public IUserService UserService { get; private set; }

        public RepoServiceApi(HttpClient http)
        {
            Customers = new RepoService<Customer>(http, nameof(Customers));

            UserService = new UserService(http);
        }
    }
}