using GraphQL.Types;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Server.GraphQL.Types
{
    public class LoginInputType : InputObjectGraphType<User>
    {
        public LoginInputType()
        {
            Name = "LoginInput";
            Field(x => x.Username);
            Field(x => x.Password);
        }
    }
}