using System.Web.Optimization;

namespace VotingSystem.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/css").Include(
				 "~/Content/css/jquery.fileupload.min.css"
				 ));

			bundles.Add(new LessBundle("~/content/bootstrap").Include(
				 "~/Content/less/bootstrap.less"
				));

			bundles.Add(new LessBundle("~/content/main").Include(
				"~/Content/less/main.less"
				));

			bundles.Add(new ScriptBundle("~/bundles/externals").Include(
				"~/Scripts/libraries/jquery.min.js",
				"~/Scripts/libraries/toastr.js",
				"~/Scripts/libraries/spin.js",
				"~/Scripts/libraries/jquery.signalR-2.1.0.min.js",
				"~/Scripts/libraries/bootstrap.js",
				"~/Scripts/libraries/bootstrapValidator.js",
				"~/Scripts/libraries/jquery.ui.widget.js",
				"~/Scripts/libraries/jquery.iframe-transport.js",
				"~/Scripts/libraries/jquery.fileupload.js",
				"~/Scripts/libraries/moment-2.4.0.js",
				"~/Scripts/libraries/bootstrap-datetimepicker.js",
				"~/Scripts/libraries/jquery.bootpag.js",
				"~/Scripts/libraries/jquery.pagedown-bootstrap.combined.js"
				).ForceOrdered());

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
				"~/Scripts/libraries/angular/angular.min.js",
				"~/Scripts/libraries/angular/angular-route.min.js",
				"~/Scripts/libraries/angular/angular-resource.min.js"
				).ForceOrdered());

			bundles.Add(new ScriptBundle("~/bundles/client/directives").Include(
				"~/Scripts/user-defined/directives/bootstrapMarkdown.js",
				"~/Scripts/user-defined/directives/dateTimePicker.js",
				"~/Scripts/user-defined/directives/focusOut.js",
				"~/Scripts/user-defined/directives/goBack.js",
				"~/Scripts/user-defined/directives/notFound.js",
				"~/Scripts/user-defined/directives/onEnterPress.js",
				"~/Scripts/user-defined/directives/paginator.js",
				"~/Scripts/user-defined/directives/pieChart.js",
				"~/Scripts/user-defined/directives/trustHtml.js",
				"~/Scripts/user-defined/directives/validateField.js",
				"~/Scripts/user-defined/directives/validateForm.js",
				"~/Scripts/user-defined/directives/DirectivesModule.js"
				).ForceOrdered());

			bundles.Add(new ScriptBundle("~/bundles/client/controllers").Include(
				"~/Scripts/user-defined/controllers/AdminVotingsCtrl.js",
				"~/Scripts/user-defined/controllers/CommentsCtrl.js",
				"~/Scripts/user-defined/controllers/LayoutCtrl.js",
				"~/Scripts/user-defined/controllers/MainCtrl.js",
				"~/Scripts/user-defined/controllers/UserProfileCtrl.js",
				"~/Scripts/user-defined/controllers/UsersCtrl.js",
				"~/Scripts/user-defined/controllers/UserVotingsCtrl.js",
				"~/Scripts/user-defined/controllers/VoicesCtrl.js",
				"~/Scripts/user-defined/controllers/VotingCtrl.js",
				"~/Scripts/user-defined/controllers/ControllersModule.js"
			).ForceOrdered());

			bundles.Add(new ScriptBundle("~/bundles/client/services").Include(
				"~/Scripts/user-defined/services/commentsHub.js",
				"~/Scripts/user-defined/services/reload.js",
				"~/Scripts/user-defined/services/CommentStorage.js",
				"~/Scripts/user-defined/services/UserStorage.js",
				"~/Scripts/user-defined/services/VoiceStorage.js",
				"~/Scripts/user-defined/services/VotingStorage.js",
				"~/Scripts/user-defined/services/ServicesModule.js"
			).ForceOrdered());

			bundles.Add(new ScriptBundle("~/bundles/client/app").Include(
				"~/Scripts/user-defined/constants.js",
				"~/Scripts/user-defined/app.js"
			).ForceOrdered());

			BundleTable.EnableOptimizations = false;
		}
	}
}