define(["jquery", "view", "voiceModel", "stickit"],
	function ($, View, VoiceModel) {
		return  View.extend({
			className:"form-group",
			templateName: "OpenQuestionTemplate",
			
			bindings: {
				"input[type='text']": "AnswerText"
			},
			
			afterRender: function () {
				this.stickit();
			},
			
			getAnswer: function () {
				return new VoiceModel({
					AnswerText: this.model.get("AnswerText"),
					QuestionId: this.model.get("QuestionId"),
				});
			}
		});
	});