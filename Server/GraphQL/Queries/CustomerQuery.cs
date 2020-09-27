using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Poisn.GraphQL.Server.Context;
using Poisn.GraphQL.Server.GraphQL.Types;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace Poisn.GraphQL.Server.GraphQL.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        public CustomerQuery(Defer<IStore> store, Defer<ApplicationDbContext> appDb)
        {
            var again = appDb.Value.Customers.ToList();
            using var session = store.Value.CreateSession();

            var test = session.Query<Customer>().ListAsync().Result.ToList();

            Name = "Query";
            Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => test);
            Field<CustomerGraphType>("customer", "Returns a Single Customer",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
                    context => test.Single(x => x.Id == context.Arguments["id"].GetPropertyValue<int>()));
        }
    }
}