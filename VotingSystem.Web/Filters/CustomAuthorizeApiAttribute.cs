using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Helpers;

namespace VotingSystem.Web.Filters
{
	public class CustomAuthorizeApiAttribute : AuthorizeAttribute
	{
		public new RoleType[] Roles { get; set; }

		public CustomAuthorizeApiAttribute()
		{
			Roles = new RoleType[] { };
		}

		// REVIEW: use this constructor everywhere you can do it
		public CustomAuthorizeApiAttribute(RoleType roleType)
		{
			Roles = new [] { roleType };
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (!IsAuthorized(actionContext))
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
		}

		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			return AuthorizeAttributeHelper.IsAuthorizedLog(AuthorizeAttributeHelper.IsAuthorized(Roles));
		}
	}
}