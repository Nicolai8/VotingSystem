using System;
using System.Linq;

namespace VotingSystem.Common
{
	public class FilterExtended<T> : Filter
	{
		public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }

		public FilterExtended()
		{
			OrderBy = null;
		}

		public FilterExtended(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pageSize)
			: base(page, pageSize)
		{
			OrderBy = orderBy;
		}
	}
}
