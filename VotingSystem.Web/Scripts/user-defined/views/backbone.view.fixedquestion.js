define(["jquery", "view", "voiceModel","templates"],
	function ($, View, VoiceModel, templates) {
		return View.extend({
			className: "form-group",
			templateName: "FixedQuestionTemplate",

			afterRender: function () {
				var template = templates.get("FixedAnswerTemplateForVotingPage");
				var $fixedAnswers = this.$el.find(".radio").parent();
				_.each(this.model.get("FixedAnswers"), function (fixedAnswer) {
					var $fixedAnswer = $(template(fixedAnswer));
					$fixedAnswers.append($fixedAnswer);
				});
			},

			getAnswer: function () {
				return new VoiceModel({
					AnswerText: this.$el.find("input:radio:checked").data("answertext"),
					FixedAnswerId: this.$el.find("input:radio:checked").val(),
					QuestionId: this.model.get("QuestionId"),
				});
			}
		});
	});