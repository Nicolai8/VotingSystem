using System;
using System.Linq.Expressions;

namespace VotingSystem.DAL.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		RepositoryQueryHelper<TEntity> Query();

		void Delete(object id);

		void Delete(TEntity entityToDelete);

		TEntity GetById(object id);

		void Insert(TEntity entity);

		void Update(TEntity entityToUpdate);

		int GetTotal(Expression<Func<TEntity, bool>> filter = null);
	}
}