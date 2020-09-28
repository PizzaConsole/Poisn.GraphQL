using GraphQL.Types;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Server.GraphQL.Types
{
    internal class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Name = nameof(User);
            Field(x => x.Id, type: typeof(IdGraphType)).Description("User Id");
            Field(x => x.Username).Description("User's Username");
        }
    }
}