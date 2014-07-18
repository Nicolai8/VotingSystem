using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VotingSystem.Web.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(VotingSystem.Web.App_Start.UnityWebActivator), "Shutdown")]

namespace VotingSystem.Web.App_Start
{
	public static class UnityWebActivator
	{
		/// <summary>Integrates Unity when the application starts.</summary>
		public static void Start()
		{
			var container = UnityDI.UnityConfig.Initialize();

			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
			FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

			// TODO: Uncomment if you want to use PerRequestLifetimeManager
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
		}

		/// <summary>Disposes the Unity container when the application is shut down.</summary>
		public static void Shutdown()
		{
			var container = UnityDI.UnityConfig.GetConfiguredContainer();
			container.Dispose();
		}
	}
}