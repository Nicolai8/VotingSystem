define(["backbone", "Urls", "questionModel"],
function (Backbone, Urls, QuestionModel) {
	return Backbone.Collection.extend({
		model: QuestionModel,
		url: function () {
			return Urls.Voices + "/" + window.state.get("votingId");
		}
	});
});