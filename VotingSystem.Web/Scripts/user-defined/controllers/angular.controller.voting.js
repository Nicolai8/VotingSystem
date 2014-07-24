﻿define(["jquery", "angular", "Urls", "constants", "toastr", "angular.route", "bootstrap"],
	function ($, angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller("VotingController", function ($scope, votingStorage, voiceStorage, commentStorage, $http, $route, $routeParams, $location) {
					$scope.votingId = $routeParams.votingId;
					$scope.pageName = "votingpage";
					$scope.$route = $route;
					$scope.$location = $location;
					$scope.$routeParams = $routeParams;
					$scope.captchaUrl = Urls.Captcha;
					$scope.newCommentText = "";
					$scope.isAnswered = false;

					votingStorage.get({ id: $routeParams.votingId },
						function (voting) {
							$scope.voting = voting;
							$scope.checkIfAnswered();
							$scope.getResults();
							$scope.loaded = true;
						});

					$scope.checkIfAnswered = function () {
						$scope.isAnswered = $scope.voting.IsAnswered
							|| (!$scope.$parent.authenticated
								&& localStorage["VotingSystem.Vote#" + $scope.voting.VotingId] == "true");
					};

					$scope.addNewComment = function () {
						commentStorage.save(
							{
								CommentText: $scope.newCommentText,
								ThemeId: $scope.voting.VotingId
							},
							function (comment) {
								$scope.voting.Comments.unshift(comment);
								$scope.newCommentText = "";
								toastr.success(constants("commentSavedMessage"));
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};

					$scope.getResults = function () {
						if ($scope.isAnswered || $scope.voting.Status == 3) {
							voiceStorage.getResults({ votingId: $scope.voting.VotingId },
								function (results) {
									preprocessResults(results);
									$scope.results = results;
								}
							);
						}
					};

					$scope.newCommentKeyPressHandler = function ($event) {
						if (($event.which == 13 || $event.which == 10) && $event.ctrlKey) {
							$scope.addNewComment();
						}
					};

					$scope.updateCaptcha = function () {
						var d = new Date();
						$("#captchaImage").attr("src", Urls.Captcha + "?" + d.getTime());
						$("#captchaText").val("");
					};

					$scope.removeComment = function (comment) {
						commentStorage.remove({ id: comment.CommentId },
							function () {
								$scope.voting.Comments.splice($scope.voting.Comments.indexOf(comment), 1);
								toastr.success(constants("commentDeletedMessage"));
							}, function () {
								toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
							});
					};

					$scope.vote = function () {
						var voices = $scope.voting.Questions.map(function (question) {
							var answer = {};
							answer.AnswerText = question.AnswerText;
							answer.FixedAnswerId = question.FixedAnswerId;
							answer.QuestionId = question.QuestionId;
							return answer;
						});
						voiceStorage.save({ captcha: $scope.captcha }, voices).$promise
							.then(function (response) {
								if (!$scope.$parent.authenticated) {
									localStorage["VotingSystem.Vote#" + $scope.voting.VotingId] = "true";
									localStorage["VotingSystem.Vote#" + $scope.voting.VotingId + ".Answers"] = JSON.stringify(voices);
								}
								$("#votingModal").modal("hide");

								$scope.isAnswered = $scope.voting.IsAnswered = true;
								$scope.voting.TotalVotes = response.total;
								$scope.getResults();
								toastr.success(constants("answerSavedMessage"));
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							}).then(function () {
								$scope.updateCaptcha();
							});
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
				});

		};
	})
