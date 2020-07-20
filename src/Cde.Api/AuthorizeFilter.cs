using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cde.Api
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim) {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            // var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated) {
                foreach (var item in _claim) {
                    //If claimsIndentity does't contain the item specifiend on Authorize return error
                    if (!context.HttpContext.User.HasClaim(ClaimTypes.Role, item)) {
                        context.Result = new ForbidResult();
                    }
                }   
            } else {
                context.Result = new UnauthorizedResult();
            }
            return;
        }
    }
}
