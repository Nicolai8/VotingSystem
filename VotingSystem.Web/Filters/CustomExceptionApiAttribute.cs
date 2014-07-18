using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;
using VotingSystem.Common;

namespace VotingSystem.Web.Filters
{
	public class CustomExceptionApiAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			HandleException(actionExecutedContext.Exception);
			if (actionExecutedContext.Exception is AuthenticationException)
			{
				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
			else
			{
				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
			}
		}

		private void HandleException(Exception exception)
		{
			if (exception is VotingSystemException)
			{
				Logger.Warn(exception.Message);
			}
			else
			{
				Logger.Error(exception.Message, exception);
			}
			if (exception.InnerException != null)
			{
				HandleException(exception.InnerException);
			}
		}
	}
}