using System;
using System.Collections.Generic;
using System.Text;

namespace Poisn.GraphQL.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Password { get; set; }
    }
}