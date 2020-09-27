using GraphQL.Types;
using GraphQL.Utilities;
using Poisn.GraphQL.Server.Context;
using Poisn.GraphQL.Server.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Server.GraphQL
{
    public class DemoSchema : Schema
    {
        public DemoSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CustomerQuery>();
        }
    }
}