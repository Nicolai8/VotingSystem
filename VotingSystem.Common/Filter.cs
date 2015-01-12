using System;
using System.Linq;

namespace VotingSystem.Common
{
	public class Filter<T>
	{
		public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set;}

		public Filter()
		{
			Page = 1;
			PageSize = 10;
			OrderBy = null;
		}

		public Filter(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
			OrderBy = orderBy;
		} 
	}
}
