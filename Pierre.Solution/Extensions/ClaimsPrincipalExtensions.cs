using System.Linq;
using System.Security.Claims;

namespace PierresBakery.Extensions
{
	public static class ClaimsPrincipalExtension
	{
		public static string GetFullName(this ClaimsPrincipal principal)
		{
			var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
			return fullName?.Value;
		}
		public static string GetLastTimeVisiting(this ClaimsPrincipal principal)
		{
			var lastTime = principal.Claims.FirstOrDefault(c => c.Type == "LastTimeVisiting");
			return lastTime?.Value;
		}
	}
}