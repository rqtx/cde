using Cde.Api.Constants;
using System.Security.Claims;

namespace Cde.Api.Helpers
{
	public static class UserHelper
	{
		public static bool IsAdim(ClaimsPrincipal user) {
			if(Roles.Admin == user.FindFirst(ClaimTypes.Role).Value) {
				return true;
			}
			return false;
		}
	}
}
