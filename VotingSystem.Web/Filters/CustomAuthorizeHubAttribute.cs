using Microsoft.AspNet.SignalR;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Filters
{
	public class CustomAuthorizeHubAttribute : AuthorizeAttribute
	{
		public new RoleType[] Roles { get; set; }

		public CustomAuthorizeHubAttribute()
		{
			Roles = new RoleType[] { };
		}

		protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
		{
			return AuthorizeAttributeHelper.IsAuthorizedLog(AuthorizeAttributeHelper.IsAuthorized(Roles));
		}
	}
}