using System.Data.Entity;
using System.Reflection;
using Microsoft.Practices.Unity;
using VotingSystem.BLL;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Repositories;

namespace VotingSystem.UnityDI
{
	public static class UnityConfig
	{
		private static IUnityContainer _container;

		public static IUnityContainer Initialize()
		{
			_container = new UnityContainer();

			_container.RegisterType<DbContext, VotingSystemContext>(new PerRequestLifetimeManager());
			_container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

			_container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new PerRequestLifetimeManager());
			_container.RegisterTypes(
				AllClasses.FromAssemblies(
					Assembly.GetAssembly(typeof(IUserService)),
					Assembly.GetAssembly(typeof(UserService))),
				WithMappings.FromMatchingInterface,
				WithName.Default, WithLifetime.PerResolve);

			return _container;
		}

		public static IUnityContainer GetConfiguredContainer()
		{
			return _container;
		}
	}
}
