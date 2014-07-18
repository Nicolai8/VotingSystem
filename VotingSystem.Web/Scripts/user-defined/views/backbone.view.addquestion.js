define(["jquery", "backbone", "underscore", "fixedAnswerModel", "fixedAnswersCollection",
		"Urls", "toastr", "constants", "templates", "stickit"],
	function ($, Backbone, _, FixedAnswerModel, FixedAnswerCollection, Urls, toastr, constants, templates) {

		var addVotingView = Backbone.View.extend({
			el: $("#addNewQuestion"),
			templateName: "NewAnswerTemplate",
			cancel: true,
			update: false,

			events: {
				"click #addNewQuestionModal #saveNewQuestion": "saveNewQuestion",
				"click #defineAnswersModal #addNewAnswerButton": "addNewAnswer",
				"click #defineAnswersModal form > div  i.fa-minus": "removeAnswer",
				"click a":"preventDefault"
			},
			bindings: {
				"#addNewQuestionModal [name='VotingText']": "Text",
				"#addNewQuestionModal input[type='radio'][name='Type']": "Type",
				"#addNewQuestionModal [role='choicequestiontype']": {
					observe: "Type",
					visible: function (value) {
						if (value == 0) {
							this.fixedAnswersCollection.reset();
							this.$el.find("#defineAnswersModal form > div").empty();
						}
						return value == 1;
					}
				}
			},

			initialize: function () {
				this.fixedAnswersCollection = new FixedAnswerCollection();
				this.stickit();
			},

			render: function () {
				this.update = true;
				if (this.model.get("Type") == 1) {
					var that = this;
					this.fixedAnswersCollection = this.model.get("FixedAnswers");
					this.fixedAnswersCollection.each(function(answer) {
						that.$el.find("#defineAnswersModal form > div")
							.append(templates.get(that.templateName)(answer.toJSON()));
					});
				}
			},
			unrender: function () {
				this.undelegateEvents();
				this.$el.removeData().unbind();
				this.$el.find("#defineAnswersModal form > div").empty();
				this.unstickit();
			},

			saveNewQuestion:function(e) {
				this.cancel = false;
				this.model.set("FixedAnswers", this.fixedAnswersCollection);
				$(e.currentTarget).closest("div.modal").modal("hide");
			},
			addNewAnswer: function () {
				var $newAnswerText = this.$el.find("#addNewAnswerText");
				var answer = new FixedAnswerModel({
					AnswerText: $newAnswerText.val(),
				});
				if ($newAnswerText.val().length > 0) {
					this.fixedAnswersCollection.add(answer);
					this.$el.find("#defineAnswersModal form > div")
						.append(templates.get(this.templateName)(answer.toJSON()));
				} else {
					toastr.info(constants("emptyFixedAnswerMessage"));
				}
				$newAnswerText.val("");
			},
			removeAnswer: function (e) {
				e.preventDefault();
				var that = this;
				$(e.currentTarget).closest("a").remove();
				this.fixedAnswersCollection.reset();
				$.each(this.$el.find("form  > div a"), function (index, value) {
					that.fixedAnswersCollection.add(new FixedAnswerModel({ AnswerText: $(value).text() }));
				});
			},
			preventDefault:function(e) {
				e.preventDefault();
			}
		});
		return addVotingView;
	});