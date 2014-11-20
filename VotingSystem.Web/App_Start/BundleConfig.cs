using System.Web.Optimization;

namespace VotingSystem.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(	
				 "~/Content/css/jquery.fileupload.min.css"
				 ));

            bundles.Add(new LessBundle("~/content/bootstrap").Include(
                 "~/Content/less/bootstrap.less"
                ));

			bundles.Add(new LessBundle("~/content/main").Include(
				"~/Content/less/main.less"
				));

			BundleTable.EnableOptimizations = true;
		}
	}
}