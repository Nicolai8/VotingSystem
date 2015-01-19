using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace VotingSystem.UnityDI
{
	public class UnityDependencyResolver : IDependencyResolver
	{
		protected readonly IUnityContainer Container;

		public UnityDependencyResolver(IUnityContainer container)
		{
			//REVIEW: everywhere we need to use { }. Pls, review all code
			if (container == null)
				throw new ArgumentNullException("container");

			Container = container;
		}

		public object GetService(Type serviceType)
		{
			return Container.IsRegistered(serviceType) ? Container.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.IsRegistered(serviceType) ? Container.ResolveAll(serviceType) : new List<object>();
		}

		public void Dispose()
		{
			Container.Dispose();
		}
	}
}