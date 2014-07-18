define(["jquery", "backbone", "underscore", "page", "bootpag"],
function ($, Backbone, _, Page) {
	var pageCollection = Page.extend({
		totalUrl: "",
		paginatorClass: ".pagePaginator",

		prerender: function () {
			var pageNumber = window.state.get("page");
			if (pageNumber != 1) {
				if (this.collection.length == 0) {
					pageNumber -= 1;
				}
			}
			if (pageNumber != window.state.get("page")) {
				window.state.set("page", pageNumber);
			} else {
				this.unrender();
				this.render();
			}
		},
		unrender: function () {
			if (this.collection.length > 0) {
				this.collection.each(function (model) {
					if (model.modelView) {
						model.modelView.unrender();
					}
				}, this);
			}
			$("#dataNotFound").empty();
		},

		getTotalUrl: function () {
			return this.totalUrl + "?query=" + window.state.get("query");
		},
		updatePaginator: function () {
			var paginator = this.$el.find(this.paginatorClass);
			$.get(this.getTotalUrl())
				.done(function (total) {
					if (total == 0) {
						total = 1;
					}
					var totalPages = Math.floor((total - 1) / window.state.get("pageSize")) + 1;

					paginator.bootpag({
						href: "#" + window.state.get("pageName") + "/{{number}}",
						total: totalPages,
						maxVisible: 10,
						page: window.state.get("page")
					});
					

					paginator.toggle(totalPages > 1);
				});
		},
	});

	return pageCollection;
})