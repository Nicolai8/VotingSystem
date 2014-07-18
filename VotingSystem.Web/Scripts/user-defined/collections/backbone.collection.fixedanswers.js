define(["backbone", "Urls", "fixedAnswerModel"],
function (Backbone, Urls, FixedAnswerModel) {
	return Backbone.Collection.extend({
		model: FixedAnswerModel,
	});
});