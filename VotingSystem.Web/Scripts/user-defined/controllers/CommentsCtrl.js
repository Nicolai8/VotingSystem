angular.module("votingSystem.controllers.comments", [])
	.controller("CommentsCtrl", ["$scope", "$routeParams", "reloadDataAfterUserAction", "UnitOfWork", "commentsHub", "notifications",
		function ($scope, $routeParams, reloadDataAfterUserAction, UnitOfWork, commentsHub, notifications) {
			$scope.pageName = "comments";
			$scope.total = 1;
			$scope.breadCrumbItemName = "Comments";

			commentsHub.changePageOnHub();

			UnitOfWork.commentStorage().query({ page: $routeParams.pageNumber },
				function (data) {
					$scope.comments = data;
					UnitOfWork.commentStorage().total(
					function (response) {
						$scope.total = response.total;
					});
				});

			$scope.removeComment = function (comment) {
				comment.$remove()
					.then(function () {
						$scope.comments.splice($scope.comments.indexOf(comment), 1);
						notifications.commentDeleted();
						reloadDataAfterUserAction($scope.comments.length, "/" + $scope.pageName + "/{pageNumber}");
					}, function () {
						notifications.deletingError();
					});
			};
		}]);

