define(["jquery", "underscore", "view", "votingResultCollection", "voicesCollection", "toastr", "constants", "templates", "goog!visualization,1,packages:[corechart]"],
	function ($, _, View, VotingResultCollection, VoicesCollection, toastr, constants, templates) {
		var voiceView = View.extend({
			initialize: function () {
				this.collection = new VotingResultCollection();
				this.isAnswered = arguments[0].isAnswered;
			},

			render: function () {
				var that = this;
				this.collection.fetch().done(function () {
					that.drawResults();
				}).fail(function () {
					toastr.error(constants("loadResultFailedMessage"));
				});
			},
			drawResults: function () {
				var that = this;
				this.collection.each(function (question) {
					var answers = new VoicesCollection(question.get("Answers"));
					var answerIndex = that.getAnswerIndex(answers, question.get("QuestionId"));
					var arrayOfResults = that.getArrayOfResults(answers, answerIndex);
					that.drawPieChart(arrayOfResults, answerIndex, question.get("Text"));
					that.drawTable(arrayOfResults, answerIndex, question.get("Text"));
				});
			},
			getArrayOfResults: function (answers, answerIndex) {
				var arrayOfResults = answers.map(function (item) {
					return [item.get("AnswerText"), item.get("Count")];
				});
				arrayOfResults = _.union([['Answer', 'Count']], arrayOfResults);
				if (answerIndex != -1) {
					arrayOfResults[answerIndex + 1] = [arrayOfResults[answerIndex + 1][0] + " (Your answer)", arrayOfResults[answerIndex + 1][1]];
				}
				return arrayOfResults;
			},
			drawPieChart: function (arrayOfResults, answerIndex, questionText) {
				var options = {
					title: questionText,
					height: 400,
					width: 700,
					sliceVisibilityThreshold: 5 / 360,
					pieSliceText: "none",
					titleTextStyle: {
						fontSize:20
					}
				};

				if (answerIndex != -1) {
					options.slices = {};
					options.slices[answerIndex] = { offset: 0.2 };
				}

				var data = google.visualization.arrayToDataTable(arrayOfResults);
				var $chart = $("<div/>");
				this.$el.find(".result-chart").append($chart);
				new google.visualization.PieChart($chart[0]).draw(data, options);
			},
			drawTable: function (arrayOfResults, answerIndex, questionText) {
				var $table = $(templates.get("VotingResultTableTemplate")(
					{
						questionText: questionText,
						firstColumn: arrayOfResults[0][0],
						secondColumn: arrayOfResults[0][1]
					}));
				var $tbody = $table.find("tbody");
				var itemTemplate = templates.get("VotingResultItemTemplate");
				answerIndex++;
				for (var i = 1; i < arrayOfResults.length; i++) {
					$tbody.append(itemTemplate({
						item: arrayOfResults[i],
						isAnswer: answerIndex == i
					}
					));
				}
				this.$el.find(".result-table").append($table);
			},
			getAnswerIndex: function (sourceArray, questionId) {
				var answerText;
				if (this.isAnswered) {
					if (window.state.get("authenticated")) {
						answerText = sourceArray.findWhere({ "IsUserAnswer": true }).get("AnswerText");
					} else {
						var localAnswers = new VoicesCollection(JSON.parse(localStorage["VotingSystem.Vote#" + window.state.get("votingId") + ".Answers"]));
						answerText = localAnswers.findWhere({ "QuestionId": questionId }).get("AnswerText");
					}
					return sourceArray.indexOf(sourceArray.findWhere({ "AnswerText": answerText }));
				}
				return -1;
			}
		});
		return voiceView;
	});