define(["jquery", "page", "votingModel", "questionModel", "questionsCollection", "openQuestionView", "fixedQuestionView",
		"commentsCollection", "commentModel", "commentView", "votingResultView", "Urls", "toastr",
		"constants", "markdown", "stickit", "bootstrap", "bootstrapValidator", "signalr", "signalr.hubs"],
	function ($, Page, VotingModel, QuestionModel, QuestionCollection, OpenQuestionView, FixedQuestionView,
		CommentsCollection, CommentModel, CommentView, VotingResultView, Urls, toastr, constants) {
		var votingPageCommentView = CommentView.extend({
			tagName: "li",
			className: "list-group-item",

			templateName: "CommentTemplateForVotingPage",

			initialize: function () {
				this.model.on("destroy", arguments[0].deleteNotificationCallback);
				this.model.on("destroy", this.unrender, this);
			},
		});

		var votingPageView = Page.extend({
			el: $("#votingPage"),
			breadCrumbItemName: "Voting",

			events: {
				"click #voteOnVoting": "vote",
				"click #addNewComment": "addNewComment",
				"keypress #newCommentText": "newCommentKeyPressHandler",
			},

			bindings: {
				"#votingPageContent [role='name']": "VotingName",
				"#votingPageContent [role='createdBy']": {
					observe: "CreatedBy",
					attributes: [{
						name: "href",
						observe: "CreatedBy",
						onGet: function (value) {
							return "#profilepage/" + value;
						}
					}]
				},
				"#votingPageContent #voteButton": {
					observe: ["IsAnswered", "VotingId", "Status"],
					visible: function (values) {
						if (values[0].toString().length > 0
							&& values[1].toString().length > 0
							&& values[2].toString().length > 0) {
							return !this.isAnswered(values[0], values[1]) && values[2] != 3;
						}
						return false;
					},
				},
				"#votingPageContent #voteButtonReplacement": {
					observe: ["IsAnswered", "VotingId", "Status"],
					visible: function (values) {
						if (values[0].toString().length > 0
							&& values[1].toString().length > 0
							&& values[2].toString().length > 0) {
							return this.isAnswered(values[0], values[1]) && values[2] != 3;
						}
						return false;
					},
				},
				"#votingPageContent [role='createDate']": "CreateDate",
				"#votingPageContent [role='timeLeft']": "TimeLeft",
				"#votingPageContent [role='startDate']": "StartDate",
				"#votingPageContent [role='totalVotes']": "TotalVotes",
				"#votingPageContent [role='description']": {
					observe: "Description",
					updateMethod: "html",
					onGet: function (value) {
						if (value) {
							return this.converter.makeHtml(value);
						}
						return "";
					}
				},
			},

			initialize: function () {
				this.model = new VotingModel();
				this.objectToFetch = this.model;
				this.converter = new Markdown.getSanitizingConverter();
				this.commentsCollection = new CommentsCollection();
				this.listenTo(this.commentsCollection, "add", this.addComment);
				window.state.on("change:pageName=votingpage", this.render, this);
				this.stickit();
				this.configureCommentsHub();
				window.state.on("change:pageName", this.changePageOnHub, this);
				this.$el.find("#votingResults").collapse({ toggle: false });
			},

			unrender: function () {
				this.commentsCollection.each(function (comment) {
					comment.commentView.unrender();
				}, this);
				this.questions.each(function (question) {
					question.questionView.unrender();
				}, this);
			},

			fillData: function () {
				this.renderComments();
				$("#withSidebar").show();
				this.drawResults();
				this.renderQuestions();
				this.$el.find("#votingResults").collapse("hide");
			},
			drawResults: function () {
				var show = false;
				if (this.model.get("IsAnswered").toString().length > 0
					&& this.model.get("VotingId").toString().length > 0) {
					show = this.isAnswered(this.model.get("IsAnswered"), this.model.get("VotingId")) || this.model.get("Status") == 3;
					if (show) {
						this.votingResultView = new VotingResultView({
							el: $("#votingResults"),
							isAnswered: this.isAnswered(this.model.get("IsAnswered"), this.model.get("VotingId"))
						});
						this.votingResultView.render();
					}
				}
				$("#votingPageContent #toggleResultsButton").toggle(this.isAnswered(show));
			},
			isAnswered: function (isAnswered, votingId) {
				return isAnswered ||
				(!window.state.get("authenticated")
					&& localStorage["VotingSystem.Vote#" + votingId] == "true");
			},
			addComment: function (comment) {
				var that = this;
				var commentsTag = this.$el.find("#comments");
				comment.commentView = new votingPageCommentView({
					model: comment,
					deleteNotificationCallback: function (model) {
						that.commentsHub.server.deleteComment(model.get("CommentId"), model.get("VotingId"));
					}
				});
				commentsTag.append(comment.commentView.render().el);
			},
			renderQuestions: function () {
				var that = this;
				var $questions = this.$el.find("#questions");

				this.questions = new QuestionCollection();
				_.each(this.model.get("Questions"), function (question) {
					var questionModel = new QuestionModel(question);
					if (question.Type == 0) {
						questionModel.questionView = new OpenQuestionView(questionModel);
					} else {
						questionModel.questionView = new FixedQuestionView(questionModel);
					}

					$questions.append(questionModel.questionView.render().el);
					that.questions.add(questionModel);
				});
				$questions.find(":radio").iCheck({
					radioClass: "iradio_flat-red"
				});
			},
			renderComments: function () {
				var commentsCollection = this.commentsCollection;
				commentsCollection.reset();
				$.each(this.model.get("Comments"), function (i, comment) {
					commentsCollection.add(comment);
				});
			},
			configureCommentsHub: function () {
				var that = this;
				this.commentsHub = $.connection.commentsHub;
				this.commentsHub.client.createComment = function (comment) {
					comment.Own = comment.CreatedBy == window.state.get("loggedUserName");
					that.commentsCollection.add(comment);
					toastr.info(constants("commentAddedMessage"));
				};
				this.commentsHub.client.deleteComment = function (commentId) {
					var comment = that.commentsCollection.findWhere({ CommentId: commentId });
					if (typeof comment !== "undefined") {
						that.commentsCollection.remove(comment);
						comment.commentView.unrender();
						toastr.info(constants("commentDeletedTemplateMessage").replace("{0}", comment.get("CommentText")));
					}
				};
				this.configureCommentsHubHandlers();
			},
			configureCommentsHubHandlers: function () {
				var that = this;
				$.connection.hub.start().done(function () {
					that.changePageOnHub();
				});
			},
			changePageOnHub: function () {
				if ($.connection.hub.state == $.signalR.connectionState.connected) {
					var votingId = "";
					if (window.state.get("pageName") == "votingpage") {
						votingId = window.state.get("votingId");
					}
					this.commentsHub.server.changePage(votingId);
				}
			},
			stopCommentsHub: function () {
				$.connection.hub.stop();
			},
			startCommentsHub: function () {
				$.connection.hub.start();
			},

			vote: function () {
				var voices = this.questions.getAnswers();
				var that = this;
				voices.captcha = this.$el.find("#captchaText").val();
				voices.save({
					success: function (total) {
						if (!window.state.get("authenticated")) {
							localStorage["VotingSystem.Vote#" + that.model.get("VotingId")] = "true";
							localStorage["VotingSystem.Vote#" + that.model.get("VotingId") + ".Answers"] = JSON.stringify(voices.toJSON());
						}
						$("#votingModal").modal("hide");

						that.model.set("IsAnswered", true);
						that.model.set("TotalVotes", total);
						that.drawResults();
						toastr.success(constants("answerSavedMessage"));
					},
					error: function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					},
					complete:function() {
						that.updateCaptcha();
					}
				});
			},
			addNewComment: function (e) {
				e.preventDefault();
				var commentsCollection = this.commentsCollection;
				var $newCommentText = this.$el.find("#newCommentText");
				var comment = new CommentModel({
					CommentText: $newCommentText.val(),
					ThemeId: this.model.get("VotingId")
				});
				var that = this;
				comment.save(null, {
					wait: true,
					success: function (model) {
						$newCommentText.val("");
						commentsCollection.add(model);
						toastr.success(constants("commentSavedMessage"));
						that.commentsHub.server.createComment(model);
						$newCommentText.closest("form[data-validate-form='true']")
							.data("bootstrapValidator").resetForm();
					},
					error: function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					}
				});

			},
			newCommentKeyPressHandler: function (e) {
				if ((e.which == 13 || e.which == 10) && e.ctrlKey) {
					this.addNewComment(e);
				}
			},
			updateCaptcha: function () {
				var d = new Date();
				$("#captchaImage").attr("src", Urls.Captcha + "?" + d.getTime());
				$("#captchaText").val("");
			},
		});

		return votingPageView;
	});