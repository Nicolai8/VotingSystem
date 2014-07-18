define(["jquery", "underscore", "backbone", "bootstrap", "bootstrapValidator"],
function ($, _, Backbone) {
	var router = Backbone.Router.extend({
		initialize: function () {
			var that = this;
			this.routesHit = 0;
			Backbone.history.on("route", function () {
				that.routesHit++;
			}, this);
		},

		routes: {
			"": "mainPage",
			":pageName(/:parameter)(/:query)": "defaultRoute"
		},

		mainPage: function () {
			var args = {
				pageName: "mainpage",
			};
			window.state.set(args);
		},

		defaultRoute: function (pageName, parameter, query) {
			var args = {
				pageName: _.contains(window.pages, pageName) ? pageName : "page404",
				page: parameter,
				votingId: parameter,
				userName: parameter,
			};
			if (typeof parameter === "undefined" || parameter == null) {
				args.page = 1;
				args.votingId = 1;
				args.userName = "";
			}
			args.query = typeof query === "undefined" || query == null ? "" : query;

			this.closeAllOpenPages();
			window.state.set(args);
		},

		back: function () {
			if (this.routesHit > 1) {
				history.back();
			} else {
				this.navigate('mainpage', { trigger: true });
			}
		},

		closeAllOpenPages: function () {
			$("#breadcrumb").empty();
			$(".open-page").removeClass("open-page").hide();
			$(".destroy-on-page-close").empty();
			$("#withSidebar").hide();
			$("form[data-validate-form='true'], form[data-validate-form='false']").each(function () {
				var validateForm = $(this).data("bootstrapValidator");
				if (!_.isUndefined(validateForm)) {
					validateForm.resetForm();
				}
			});
			$(".modal.in[role=\"dialog\"]").modal("hide");
		}
	});

	return router;
});