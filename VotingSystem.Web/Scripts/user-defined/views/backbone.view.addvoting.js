define(["jquery", "backbone", "underscore", "votingModel", "questionModel", "questionsCollection", "addQuestionView",
		"Urls", "toastr", "constants", "templates", "stickit"],
function ($, Backbone, _, VotingModel, QuestionModel, QuestionsCollection, AddQuestionView, Urls, toastr, constants, templates) {

	var addVotingView = Backbone.View.extend({
		el: $("#addNewVoting"),
		templateName: "NewQuestionTemplate",
		cancel: true,

		events: {
			"click #addNewVotingButton": "addNewVoting",
			"click #defineQuestionsModal #addNewQuestionButton": "renderNewQuestionModal",
			"hide.bs.modal #addNewQuestionModal": "unrenderNewQuestionModal",
			"click #defineQuestionsModal form > div.list-group i.fa-minus": "removeQuestion",
			"click #defineQuestionsModal form > div.list-group a": "openQuestion"
		},
		bindings: {
			"#addNewVotingModal #newVotingName": "VotingName",
			"#addNewVotingModal [name='Description']": "Description",
		},

		initialize: function () {
			this.model = new VotingModel();
			this.questionsCollection = new QuestionsCollection();
			this.stickit();
		},

		unrender: function () {
			this.undelegateEvents();
			this.$el.removeData().unbind();
			this.$el.find("#defineQuestionsModal form > div.list-group").empty();
			this.unstickit();
		},

		addNewVoting: function () {
			var that = this;
			this.model.set("Questions", this.questionsCollection);
			this.model.set(
				{
					StartDate: this.$el.find("#newVotingStartDate :text").val(),
					FinishTime: this.$el.find("#newVotingFinishDate :text").val()
				});
			this.model.save(null, {
				wait: true,
				success: function () {
					that.cancel = false;
					that.$el.find("#addNewVotingModal").modal("hide");
					toastr.success(constants("votingCreatedMessage"));
				},
				error: function () {
					toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
				}
			});
		},

		renderNewQuestionModal: function () {
			this.addQuestionView = new AddQuestionView({
				model: new QuestionModel()
			});
		},
		unrenderNewQuestionModal: function () {
			if (!this.addQuestionView.cancel) {
				var $questions = this.$el.find("#defineQuestionsModal form > div.list-group");
				if (this.addQuestionView.update) {
					this.questionsCollection.remove(this.questionsCollection.findWhere({ QuestionId: this.addQuestionView.model.get("QuestionId") }));
					this.questionsCollection.add(this.addQuestionView.model);
					$questions.find("[data-id='" + this.addQuestionView.model.get("QuestionId") + "']")
						.replaceWith(templates.get(this.templateName)(this.addQuestionView.model.toJSON()));
				} else {
					var questionId = 1;
					if (this.questionsCollection.length > 0) {
						questionId = this.questionsCollection.max(function (question) {
							return question.get("QuestionId");
						}).get("QuestionId") + 1;
					}
					this.addQuestionView.model.set("QuestionId", questionId);
					this.questionsCollection.add(this.addQuestionView.model);
					$questions.append(templates.get(this.templateName)(this.addQuestionView.model.toJSON()));
				}
			}
			this.addQuestionView.unrender();
		},
		removeQuestion: function (e) {
			e.preventDefault();
			e.stopPropagation();
			var $question = $(e.currentTarget);
			this.questionsCollection.remove(this.questionsCollection.findWhere({ QuestionId: $question.data("id") }));
			$question.closest("a").remove();
		},
		openQuestion: function (e) {
			e.preventDefault();
			var $question = $(e.currentTarget);
			this.addQuestionView = new AddQuestionView({
				model: this.questionsCollection.findWhere({ QuestionId: $question.data("id") })
			});
			this.addQuestionView.render();
			this.$el.find("#addNewQuestionModal").modal("show");
		}
	});
	return addVotingView;
});