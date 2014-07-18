define(["backbone", "questionModel", "voicesCollection"],
function (Backbone, QuestionModel, VoicesCollection) {
	return Backbone.Collection.extend({
		model: QuestionModel,

		getAnswers: function () {
			return new VoicesCollection(this.map(function (question) {
				return question.questionView.getAnswer();
			}));
		}
	});
});