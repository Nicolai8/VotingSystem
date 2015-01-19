using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace VotingSystem.UnityDI
{
	public class UnityControllerFactory : DefaultControllerFactory
	{
		private readonly IUnityContainer _container;

		public UnityControllerFactory(IUnityContainer container)
		{
			_container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			//REVIEW: everywhere we need to use { }
			if (controllerType == null)
				return null;

			try
			{
				//REVIEW: everywhere we need to use { }
				if (!typeof(IController).IsAssignableFrom(controllerType))
					throw new ArgumentException(string.Format("Type requested is not a controller: {0}",
																		controllerType.Name),
																		"controllerType");
				return _container.Resolve(controllerType) as IController;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
