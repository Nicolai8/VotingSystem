using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using VotingSystem.Common;
using VotingSystem.Web.Filters;

namespace VotingSystem.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionApiAttribute());
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();
			HandleException(ex);
			if (Request.Headers["X-Requested-With"] != null && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				Response.Clear();
				Server.ClearError();
				Response.StatusCode = 500;
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

		public override void Init()
		{
			PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
			base.Init();
		}

		void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
		{
			HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
		}
	}
}