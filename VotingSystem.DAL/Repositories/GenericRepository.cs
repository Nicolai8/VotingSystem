using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace VotingSystem.DAL.Repositories
{
	public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		public GenericRepository(VotingSystemContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		private readonly DbSet<TEntity> _dbSet;
		private readonly VotingSystemContext _context;

		internal IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			List<Expression<Func<TEntity, object>>> includeProperties = null,
			int? page = null, int? pageSize = null)
		{
			IQueryable<TEntity> query = filter != null ? _context.Set<TEntity>().Where(filter) : _context.Set<TEntity>();

			if (includeProperties != null)
			{
				includeProperties.ForEach(i => { query = query.Include(i); });
			}
			if (orderBy != null)
			{
				query = orderBy(query);
			}
			if (page != null && pageSize != null)
			{
				query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
			}
			return query;
		}

		public RepositoryQueryHelper<TEntity> Query()
		{
			return new RepositoryQueryHelper<TEntity>(this);
		}

		public void Delete(object id)
		{
			TEntity entityToDelete = _dbSet.Find(id);
			_dbSet.Remove(entityToDelete);
		}

		public void Delete(TEntity entityToDelete)
		{
			if (_context.Entry(entityToDelete).State == EntityState.Detached)
			{
				_dbSet.Attach(entityToDelete);
			}
			_dbSet.Remove(entityToDelete);

		}

		public TEntity GetById(object id)
		{
			return _dbSet.Find(id);
		}

		public void Insert(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Update(TEntity entityToUpdate)
		{
			if (_context.Entry(entityToUpdate).State == EntityState.Detached)
			{
				_dbSet.Attach(entityToUpdate);
			}
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public int GetTotal(Expression<Func<TEntity, bool>> filter = null)
		{
			return filter != null ? _dbSet.Count(filter) : _dbSet.Count();
		}
	}
}
