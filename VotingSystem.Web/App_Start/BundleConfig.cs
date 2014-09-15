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
				"~/Content/css/bootstrap.min.css",
				"~/Content/css/bootstrapValidator.min.css",
				 "~/Content/css/jquery.fileupload.min.css"
				 ));

			bundles.Add(new LessBundle("~/content/main").Include(
				"~/Content/site.less"
				));

			bundles.Add(new LessBundle("~/bundles/less").Include(
				"~/Content/toastr.less",
				"~/Content/font-awesome.less"
				));

			BundleTable.EnableOptimizations = true;
		}
	}
}