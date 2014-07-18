define(["backbone", "Urls", "userModel"],
function (Backbone, Urls, UserModel) {
	return Backbone.Collection.extend({
		model: UserModel,
		url: function () {
			return Urls.Users + "/" + this.pageType.replace(" ", "") + "/" + window.state.get("page")
				+ "?size=" + window.state.get("pageSize");
		}
	});
});