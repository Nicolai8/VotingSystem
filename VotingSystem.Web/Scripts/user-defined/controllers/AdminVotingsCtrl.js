angular.module("votingSystem.controllers.adminVotings", [])
	.controller("AdminVotingsCtrl", [
		"$scope", "$routeParams", "reloadDataAfterUserAction", "UnitOfWork", "commentsHub", "notifications",
		function ($scope, $routeParams, reloadDataAfterUserAction, UnitOfWork, commentsHub, notifications) {
			$scope.breadCrumbItemName = "Admin Votings";
			$scope.pageName = "adminVotings";
			$scope.total = 1;

			commentsHub.changePageOnHub();

			UnitOfWork.votingStorage().query(
				{
					pageType: "AdminVotings",
					page: $routeParams.pageNumber
				},
				function (data) {
					$scope.votings = data;
					UnitOfWork.votingStorage().total(
						{
							totalKind: "totalAdmin"
						},
						function (response) {
							$scope.total = response.total;
						});
				});

			$scope.setVotingStatus = function (voting, status) {
				var oldStatus = angular.copy(voting.Status);
				voting.Status = status;
				voting.$update().then(
					function () {
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
		}
	]);