﻿angular.module("votingSystem.controllers.voting", [])
	.controller("VotingCtrl", ["$scope", "$routeParams", "urls", "Voting", "UnitOfWork", "commentsHub", "notifications",
		function ($scope, $routeParams, urls, Voting, UnitOfWork, commentsHub, notifications) {
			$scope.accountName = $scope.$parent.accountName;
			$scope.voting = Voting;
			$scope.breadCrumbItemName = "Voting: " + Voting.VotingName;
			$scope.pageName = "voting";
			$scope.captchaUrl = urls.Captcha;
			$scope.newCommentText = "";
			$scope.isAnswered = false;

			commentsHub.changePageOnHub($routeParams.votingId);

			$scope.checkIfAnswered = function () {
				$scope.isAnswered = $scope.voting.IsAnswered
					|| (!$scope.$parent.authenticated
					&& UnitOfWork.localStorage().isVotingAnswered($scope.voting.VotingId));
			};

			$scope.getResults = function () {
				if ($scope.isAnswered || $scope.voting.Status == 4 || $scope.voting.Status == 3) {
					UnitOfWork.voiceStorage().getResults({ votingId: $scope.voting.VotingId },
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
					UnitOfWork.commentStorage().save(
						{
							CommentText: $scope.newCommentText,
							VotingId: $scope.voting.VotingId
						},
						function (comment) {
							$scope.voting.Comments.unshift(comment);
							$scope.newCommentText = "";
							notifications.commentSaved();
							commentsHub.createComment(comment);
							$form.resetForm();
						}, function () {
							notifications.savingError();
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
				$("#captchaImage").attr("src", $scope.captchaUrl + "?" + d.getTime());
				$("#captchaText").val("");
			};

			$scope.removeComment = function (comment) {
				UnitOfWork.commentStorage().remove({ id: comment.CommentId },
					function () {
						$scope.voting.Comments.splice($scope.voting.Comments.indexOf(comment), 1);
						notifications.commentDeleted();
						commentsHub.deleteComment(comment);
					}, function () {
						notifications.deletingError();
					});
			};

			$scope.vote = function () {
				var $modal = $("#votingModal");
				var $form = $modal.find("form[data-validate-form]").data("bootstrapValidator");
				if ($form.isValid()) {
					var answers = $scope.voting.Questions.map(function (question) {
						var answer = {};
						answer.AnswerText = question.AnswerText;
						answer.FixedAnswerId = question.FixedAnswerId;
						answer.QuestionId = question.QuestionId;
						return answer;
					});
					UnitOfWork.voiceStorage().save({ captcha: $scope.captcha }, answers).$promise
						.then(function (response) {
							if (!$scope.$parent.authenticated) {
								UnitOfWork.localStorage().setVotingAnswered($scope.voting.VotingId);
								UnitOfWork.localStorage().setVotingAnswers($scope.voting.VotingId, answers);
							}
							$modal.modal("hide");

							$scope.isAnswered = $scope.voting.IsAnswered = true;
							$scope.voting.TotalVotes = response.total;
							$scope.getResults();
							notifications.answerSaved();
						}, function () {
							notifications.savingError();
						}).then(function () {
							$scope.updateCaptcha();
						});
				} else {
					$form.validate();
					notifications.invalidAnswer();
				}
			};

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
					var localAnswers = UnitOfWork.localStorage().getVotingAnswers($scope.voting.VotingId);
					answerText = $.grep(localAnswers, function (item) {
						return item.QuestionId == questionId;
					})[0].AnswerText;
				}
				return $.map(answers, function (item) {
					return item.AnswerText;
				}).indexOf(answerText);
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

			function preprocessResults(results) {
				angular.forEach(results, function (result) {
					var index = getAnswerIndex(result.Answers, result.QuestionId);
					result.Answers = getArrayOfResults(result.Answers, index);
					result.AnswerIndex = index;
				});
			}

			commentsHub.setCreateCommentHandler(function (comment) {
				var scope = angular.element("#votingPage").scope();
				comment.Own = comment.CreatedBy == scope.accountName;
				notifications.commentAdded();
				scope.voting.Comments.unshift(comment);
				scope.$apply();
			});

			commentsHub.setDeleteCommentHandler(function (commentId) {
				var scope = angular.element("#votingPage").scope();
				var comment = $.grep(scope.voting.Comments, function (item) {
					return item.CommentId == commentId;
				})[0];
				if (angular.isDefined(comment)) {
					scope.voting.Comments.splice(scope.voting.Comments.indexOf(comment), 1);
					scope.$apply();
					notifications.commentDeleted(comment.CommentText);
				}
			});
		}]);