﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Filters
{
	public class CustomAuthorizeApiAttribute : AuthorizeAttribute
	{
		public new RoleType[] Roles { get; set; }

		public CustomAuthorizeApiAttribute()
		{
			Roles = new RoleType[] { };
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