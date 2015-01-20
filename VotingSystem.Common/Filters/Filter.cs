namespace VotingSystem.Common.Filters
{
	public class Filter
	{
		public int Page { get; set; }
		public int PageSize { get; set; }

		public Filter()
		{
			Page = 1;
			PageSize = 10;
		}

		public Filter(int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}
	}
}
