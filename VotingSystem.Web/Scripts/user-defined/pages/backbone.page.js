define(["jquery", "backbone", "underscore", "templates"], function ($, Backbone, _, templates) {
	var page = Backbone.View.extend({
		breadCrumbTemplateName: "BreadCrumbTemplate",
		breadCrumbItemName: "",

		render: function () {
			var that = this;
			window.state.currentPage = this;

			this.objectToFetch.fetch()
				.done(function () {
					that.updateBreadCrumb();
					that.$el.addClass("open-page").show();
					that.fillData();
				}).fail(function (context) {
					window.router.navigate(context.status == 403 ? "page403" : "page500", { trigger: true });
				});
		},

		fillData: function () { },

		updateBreadCrumb: function () {
			$("#breadcrumb").html(templates.get(this.breadCrumbTemplateName)({ Name: this.breadCrumbItemName }));
		},
	});

	return page;
})