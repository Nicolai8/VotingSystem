using System.Net;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Filters
{
	public class CustomAuthorizeMvcAttribute : AuthorizeAttribute
	{
		public new RoleType[] Roles { get; set; }

		public CustomAuthorizeMvcAttribute()
		{
			Roles = new RoleType[] { };
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return AuthorizeAttributeHelper.IsAuthorizedLog(AuthorizeAttributeHelper.IsAuthorized(Roles));
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
		}
	}
}