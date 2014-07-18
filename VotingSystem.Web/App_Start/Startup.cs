using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;
using VotingSystem.Web.Controllers.Hubs;

[assembly: OwinStartup(typeof(VotingSystem.Web.App_Start.Startup))]
namespace VotingSystem.Web.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			GlobalHost.DependencyResolver.Register(typeof(CommentsHub),
				() => new CommentsHub());

			app.MapSignalR();
		}
	}
}