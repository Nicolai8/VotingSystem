define(["jquery", "angular", "Urls", "constants", "toastr", "angular.route", "bootstrap", "signalr", "signalr.hubs"],
	function ($, angular, Urls, constants, toastr) {
		angular.module("votingSystem.controllers.voting", [])
			.controller("VotingCtrl", function ($scope, $http, $route, $routeParams, $location, Voting, VotingStorage, VoiceStorage, CommentStorage, commentsHub) {
				$scope.accountName = $scope.$parent.accountName;
				$scope.voting = Voting;
				$scope.votingId = $routeParams.votingId;
				$scope.breadCrumbItemName = "Voting: " + Voting.VotingName;
				$scope.pageName = "votingpage";
				$scope.$route = $route;
				$scope.$location = $location;
				$scope.$routeParams = $routeParams;
				$scope.captchaUrl = Urls.Captcha;
				$scope.newCommentText = "";
				$scope.isAnswered = false;
				$scope.commentsHub = commentsHub;

				$scope.commentsHub.changePageOnHub($scope.votingId);

				$scope.checkIfAnswered = function () {
					$scope.isAnswered = $scope.voting.IsAnswered
						|| (!$scope.$parent.authenticated
							&& localStorage["VotingSystem.Vote#" + $scope.voting.VotingId] == "true");
				};

				$scope.getResults = function () {
					if ($scope.isAnswered || $scope.voting.Status == 3) {
						VoiceStorage.getResults({ votingId: $scope.voting.VotingId },
							function (results) {
								preprocessResults(results);
								$scope.results = results;
							}
						);
					}
				};

				$scope.checkIfAnswered();
				$scope.getResults();

				$scope.addNewComment = function (e) {
					var $form = $(e.currentTarget).closest("form[data-validate-form]").data("bootstrapValidator");
					if ($form.isValid()) {
						CommentStorage.save(
							{
								CommentText: $scope.newCommentText,
								ThemeId: $scope.voting.VotingId
							},
							function (comment) {
								$scope.voting.Comments.unshift(comment);
								$scope.newCommentText = "";
								toastr.success(constants("commentSavedMessage"));
								$scope.commentsHub.createComment(comment);
								$form.resetForm();
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					} else {
						$form.validate();
					}
				};

				$scope.newCommentKeyPressHandler = function ($event) {
					if (($event.which == 13 || $event.which == 10) && $event.ctrlKey) {
						$scope.addNewComment($event);
					}
				};

				$scope.updateCaptcha = function () {
					var d = new Date();
					$("#captchaImage").attr("src", Urls.Captcha + "?" + d.getTime());
					$("#captchaText").val("");
				};

				$scope.removeComment = function (comment) {
					CommentStorage.remove({ id: comment.CommentId },
						function () {
							$scope.voting.Comments.splice($scope.voting.Comments.indexOf(comment), 1);
							toastr.success(constants("commentDeletedMessage"));
							$scope.commentsHub.deleteComment(comment);
						}, function () {
							toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
						});
				};

				$scope.vote = function () {
					var $modal = $("#votingModal");
					var $form = $modal.find("form[data-validate-form]").data("bootstrapValidator");
					if ($form.isValid()) {
						var voices = $scope.voting.Questions.map(function (question) {
							var answer = {};
							answer.AnswerText = question.AnswerText;
							answer.FixedAnswerId = question.FixedAnswerId;
							answer.QuestionId = question.QuestionId;
							return answer;
						});
						VoiceStorage.save({ captcha: $scope.captcha }, voices).$promise
							.then(function (response) {
								if (!$scope.$parent.authenticated) {
									localStorage["VotingSystem.Vote#" + $scope.voting.VotingId] = "true";
									localStorage["VotingSystem.Vote#" + $scope.voting.VotingId + ".Answers"] = JSON.stringify(voices);
								}
								$modal.modal("hide");

								$scope.isAnswered = $scope.voting.IsAnswered = true;
								$scope.voting.TotalVotes = response.total;
								$scope.getResults();
								toastr.success(constants("answerSavedMessage"));
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							}).then(function () {
								$scope.updateCaptcha();
							});
					} else {
						$form.validate();
						toastr.info(constants("invalidAnswerMessage"));
					}
				};

				function preprocessResults(results) {
					angular.forEach(results, function (result) {
						var index = getAnswerIndex(result.Answers, result.QuestionId);
						result.Answers = getArrayOfResults(result.Answers, index);
						result.AnswerIndex = index;
					});
				}

				function getArrayOfResults(answers, answerIndex) {
					var arrayOfResults = $.map(answers, function (item) {
						return [[item.AnswerText, item.Count]];
					});
					if (answerIndex != -1) {
						arrayOfResults[answerIndex] = [arrayOfResults[answerIndex][0] + " (Your answer)", arrayOfResults[answerIndex][1]];
					}
					return arrayOfResults;
				}

				function getAnswerIndex(answers, questionId) {
					var answerText;
					if (!$scope.isAnswered) {
						return -1;
					}
					if ($scope.$parent.authenticated) {
						answerText = $.grep(answers, function (item) {
							return item.IsUserAnswer;
						})[0].AnswerText;
					} else {
						var localAnswers = JSON.parse(localStorage["VotingSystem.Vote#" + $scope.voting.VotingId + ".Answers"]);
						answerText = $.grep(localAnswers, function (item) {
							return item.QuestionId == questionId;
						})[0].AnswerText;
					}
					return $.map(answers, function (item) {
						return item.AnswerText;
					}).indexOf(answerText);
				}

				commentsHub.setCreateCommentHandler(function (comment) {
					comment.Own = comment.CreatedBy == $scope.accountName;
					toastr.info(constants("commentAddedMessage"));
					console.info($scope.voting.Comments.length);
					$scope.voting.Comments.unshift(comment);
					$scope.$apply();
					console.info($scope.voting.Comments.length);
				});

				commentsHub.setDeleteCommentHandler(function (commentId) {
					var comment = $.grep($scope.voting.Comments, function (item) {
						return item.CommentId == commentId;
					})[0];
					if (angular.isDefined(comment)) {
						$scope.voting.Comments.splice($scope.voting.Comments.indexOf(comment), 1);
						$scope.$apply();
						toastr.info(constants("commentDeletedTemplateMessage").replace("{0}", comment.CommentText));
					}
				});

				$scope.refresh = function() {
					$scope.$apply();
					console.info($scope.voting.Comments.length);
				};
			});
	});