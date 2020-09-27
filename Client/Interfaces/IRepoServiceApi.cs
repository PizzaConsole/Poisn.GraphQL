using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public interface IRepoServiceApi
    {
        IRepoService<Customer> Customers { get; }
        IUserService UserService { get; }
    }
}