angular.module("votingSystem.controllers.userVotings", [])
	.controller("UserVotingsCtrl", [
		"$scope", "$routeParams", "$location", "constants", "UnitOfWork", "commentsHub",
		"reloadDataAfterUserAction", "notifications",
		function ($scope, $routeParams, $location, constants, UnitOfWork, commentsHub,
			reloadDataAfterUserAction, notifications) {
			$scope.pageName = "userVotings";
			$scope.total = 1;
			$scope.breadCrumbItemName = "Votings";
			$scope.votings = [];

			commentsHub.changePageOnHub();

			UnitOfWork.votingStorage().query(
				{
					pageType: "UserVotings",
					page: $routeParams.pageNumber
				},
				function (data) {
					$scope.votings = data;
					UnitOfWork.votingStorage().total(
						{
							totalKind: "totalUser"
						},
						function (response) {
							$scope.total = response.total;
						});
				});

			$scope.setVotingStatus = function (voting, status) {
				var oldStatus = angular.copy(voting.Status);
				voting.Status = status;
				voting.$update()
					.then(function () {
						notifications.votingStatusChanged();
					}, function () {
						voting.Status = oldStatus;
						notifications.savingError();
					});
			};

			$scope.removeVoting = function (voting) {
				voting.$remove(
					function () {
						$scope.votings.splice($scope.votings.indexOf(voting), 1);
						notifications.votingDeleted();
						reloadDataAfterUserAction($scope.votings.length, "/" + $scope.pageName + "/{pageNumber}");
					},
					function () {
						notifications.deletingError();
					});
			};

			$scope.addNewVoting = function ($event, newVoting) {
				UnitOfWork.votingStorage().save(
					{}, newVoting,
					function () {
						angular.element($event.currentTarget).closest(".modal").modal("hide");
						notifications.votingCreated();
						reloadDataAfterUserAction($scope.votings.length, "/" + $scope.pageName + "/{pageNumber}");
					}, function () {
						angular.element($event.currentTarget).closest(".modal").modal("hide");
						notifications.savingError();
					});
			};

			$scope.openQuestion = function (question) {
				$scope.newQuestion = question;
				angular.element("#addNewQuestionModal").modal("show");
			};

			$scope.removeQuestion = function (question) {
				$scope.newVoting.Questions.splice($scope.newVoting.Questions.indexOf(question), 1);
			};

			$scope.addQuestionToVoting = function ($event, newQuestion) {
				if ($scope.newVoting.Questions.indexOf(newQuestion) == -1) {
					$scope.newVoting.Questions.push(angular.copy(newQuestion));
				}
				$scope.newQuestion = { FixedAnswers: [] };
				angular.element($event.currentTarget).closest(".modal").modal("hide");
			};

			$scope.addNewAnswerToQuestion = function (newAnswer) {
				$scope.newQuestion.FixedAnswers.push(newAnswer);
				$scope.newAnswer = "";
			};

			$scope.removeAnswer = function (answer) {
				$scope.newQuestion.FixedAnswers.splice($scope.newQuestion.FixedAnswers.indexOf(answer), 1);
			};

			$scope.setDate = function (property, date) {
				$scope.newVoting[property] = date;
				//REVIEW: ;
				$scope.$apply();
			}
		}
	]);