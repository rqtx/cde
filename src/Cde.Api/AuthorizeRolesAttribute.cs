using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cde.Api
{
    public class AuthorizeRolesAttribute : TypeFilterAttribute
    {
        public AuthorizeRolesAttribute(params string[] claim) : base(typeof(AuthorizeRolesFilter)) {
            Arguments = new object[] { claim };
        }
    }
}
