define(["angular", "Urls", "constants", "toastr", "angular.route", "bootstrap"],
	function (angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller("UserVotingsController", function ($scope, votingStorage, $reload, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "uservotingspage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;
					$scope.reload = $reload;
					$scope.votings = [];

					votingStorage.query(
						{
							pageType: "UserVotings",
							page: $scope.page
						},
						function (data) {
							$scope.votings = data;
							votingStorage.total(
								{
									totalKind: "totaluser"
								},
								function (response) {
									$scope.total = response.total;
								});
						});

					$scope.setThemeStatus = function (voting, status) {
						var oldStatus = angular.copy(voting.Status);
						voting.Status = status;
						voting.$update().then(
							function () {
								toastr.success(constants("votingStatusChangedMessage"));
							}, function () {
								voting.Status = oldStatus;
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};

					$scope.removeTheme = function (voting) {
						voting.$remove(
							function () {
								$scope.votings.splice($scope.votings.indexOf(voting), 1);
								toastr.success(constants("votingDeletedMessage"));
								$scope.reload($scope, "/" + $scope.pageName + "/{pageNumber}");
							},
							function () {
								toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
							});
					};

					$scope.addNewVoting = function ($event, newVoting) {
						newVoting.StartDate = $("#newVotingStartDate :text").val();
						newVoting.FinishTime = $("#newVotingFinishDate :text").val();
						votingStorage.save({}, newVoting).$promise
							.then(function () {
								angular.element($event.currentTarget).closest(".modal").modal("hide");
								toastr.success(constants("votingCreatedMessage"));
								$scope.reload($scope, "/" + $scope.pageName + "/{pageNumber}");
							}, function () {
								angular.element($event.currentTarget).closest(".modal").modal("hide");
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
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
						angular.element($event.currentTarget).closest(".modal").modal("hide");
					};

					$scope.addNewAnswerToQuestion = function (newAnswer) {
						$scope.newQuestion.FixedAnswers.push(newAnswer);
						$scope.newAnswer = "";
					};

					$scope.removeAnswer = function (answer) {
						$scope.newQuestion.FixedAnswers.splice($scope.newQuestion.FixedAnswers.indexOf(answer), 1);
					};
				});
		};
	});

