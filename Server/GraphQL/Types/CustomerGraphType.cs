using GraphQL.Types;
using Poisn.GraphQL.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Server.GraphQL.Types
{
    internal class CustomerGraphType : ObjectGraphType<Customer>
    {
        public CustomerGraphType()
        {
            Name = "Customer";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Customer Id");
            Field(x => x.FirstName, nullable: true).Description("Customer's First Name");
            Field(x => x.LastName, nullable: true).Description("Customer's Last Name");
            Field(x => x.Contact, nullable: true).Description("Customer's Contact");
            Field(x => x.Email, nullable: true).Description("Customer's Email");
        }
    }
}