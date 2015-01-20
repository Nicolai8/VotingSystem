using System;
using System.Linq;

namespace VotingSystem.Common.Filters
{
	public class Filter<T> : Filter
	{
		public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }

		public Filter()
		{
			OrderBy = null;
		}

		public Filter(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pageSize)
			: base(page, pageSize)
		{
			OrderBy = orderBy;
		}
	}
}
