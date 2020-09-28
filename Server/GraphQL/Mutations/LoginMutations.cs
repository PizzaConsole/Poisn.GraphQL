using GraphQL;
using GraphQL.Server.Common;
using GraphQL.Types;
using Poisn.GraphQL.Server.Context;
using Poisn.GraphQL.Server.GraphQL.Types;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace Poisn.GraphQL.Server.GraphQL.Mutations
{
    public class LoginMutation : ObjectGraphType
    {
        public LoginMutation(Defer<IStore> store)
        {
            Field<UserGraphType>(
              "userLogin",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "credentials" }
              ),
              resolve: context =>
              {
                  using (var session = store.Value.CreateSession())
                  {
                      //session.Save(customer);

                      var test = session.Query<Customer>().FirstOrDefaultAsync().Result;
                  }
                  return new User { Id = 1, Username = "Test" };
              });
        }
    }
}