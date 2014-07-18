define(["backbone", "Urls", "commentModel"],
function (Backbone, Urls, CommentModel) {
	return Backbone.Collection.extend({
		model: CommentModel,
		url: function () {
			return Urls.Comments + "?page=" + window.state.get("page")
				+ "&size=" + window.state.get("pageSize");
		}
	});
});