using System.Security.Principal;
using Microsoft.AspNet.SignalR;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Helpers;

namespace VotingSystem.Web.Filters
{
	public class CustomAuthorizeHubAttribute : AuthorizeAttribute
	{
		public new RoleType[] Roles { get; set; }

		public CustomAuthorizeHubAttribute()
		{
			Roles = new RoleType[] { };
		}

		protected override bool UserAuthorized(IPrincipal user)
		{
			return AuthorizeAttributeHelper.IsAuthorizedLog(AuthorizeAttributeHelper.IsAuthorized(Roles));
		}
	}
}