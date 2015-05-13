using System.Collections.Generic;
using System.Web.Optimization;

namespace VotingSystem.Web
{
	public class AsIsBundleOrderer : IBundleOrderer
	{
		public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			return files;
		}
	}

	public static class BundleExtensions
	{
		public static Bundle ForceOrdered(this Bundle sb)
		{
			sb.Orderer = new AsIsBundleOrderer();
			return sb;
		}
	}
}