using Microsoft.Practices.Unity;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Repositories;

namespace VotingSystem.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly VotingSystemContext _context;
		private readonly IUnityContainer _container;

		public UnitOfWork(VotingSystemContext context, IUnityContainer container)
		{
			_context = context;
			_container = container;
		}

		public IGenericRepository<Answer> AnswerRepository
		{
			get
			{
				return GetGenericRepository<Answer>();
			}
		}

		public IGenericRepository<Comment> CommentRepository
		{
			get
			{
				return GetGenericRepository<Comment>();
			}
		}

		public IGenericRepository<Voting> VotingRepository
		{
			get
			{
				return GetGenericRepository<Voting>();
			}
		}

		public IGenericRepository<User> UserRepository
		{
			get
			{
				return GetGenericRepository<User>();
			}
		}

		public IGenericRepository<Role> RoleRepository
		{
			get
			{
				return GetGenericRepository<Role>();
			}
		}

		public IGenericRepository<UserProfile> UserProfileRepository
		{
			get
			{
				return GetGenericRepository<UserProfile>();
			}
		}

		private IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : class
		{
			return _container.Resolve<IGenericRepository<TEntity>>();
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
