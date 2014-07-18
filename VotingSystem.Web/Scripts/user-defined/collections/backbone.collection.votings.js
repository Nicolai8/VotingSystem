define(["backbone", "Urls", "votingModel"],
function (Backbone, Urls, VotingModel) {
	return Backbone.Collection.extend({
		model: VotingModel,
		url: function () {
			return Urls.Votings + "/" + this.pageType + "/" + window.state.get("page")
				+ "/" + window.state.get("query") + "?size=" + window.state.get("pageSize");
		}
	});
});