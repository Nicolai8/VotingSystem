using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VotingSystem.DAL.Repositories
{
	public class RepositoryQueryHelper<TEntity> where TEntity : class
	{
		private readonly List<Expression<Func<TEntity, object>>>
			_includeProperties;

		private readonly GenericRepository<TEntity> _repository;
		private Expression<Func<TEntity, bool>> _filter;
		private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
		private int? _page;
		private int? _pageSize;

		internal RepositoryQueryHelper(GenericRepository<TEntity> repository)
		{
			_repository = repository;
			_includeProperties = new List<Expression<Func<TEntity, object>>>();
		}

		public RepositoryQueryHelper<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
		{
			_filter = filter;
			return this;
		}

		public RepositoryQueryHelper<TEntity> OrderBy(
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			_orderBy = orderBy;
			return this;
		}

		public RepositoryQueryHelper<TEntity> Include(
			Expression<Func<TEntity, object>> expression)
		{
			_includeProperties.Add(expression);
			return this;
		}

		public IEnumerable<TEntity> GetPage(
			int page, int pageSize)
		{
			_page = page;
			_pageSize = pageSize;
			//totalCount = _repository.Get(_filter).Count();
			return _repository.Get(_filter, _orderBy, _includeProperties, _page, _pageSize);
		}

		public IEnumerable<TEntity> Get()
		{
			return _repository.Get(_filter, _orderBy, _includeProperties, _page, _pageSize);
		}
	}
}
