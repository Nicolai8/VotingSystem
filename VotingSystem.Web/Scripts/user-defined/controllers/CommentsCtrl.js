angular.module("votingSystem.controllers.comments", [])
	.controller("CommentsCtrl", ["$scope", "$http", "$route", "$routeParams", "$location", "constants", "reload", "CommentStorage", "commentsHub",
		function ($scope, $http, $route, $routeParams, $location, constants, reload, CommentStorage, commentsHub) {
			$scope.page = $routeParams.pageNumber;
			$scope.pageName = "comments";
			$scope.total = 1;
			$scope.breadCrumbItemName = "Comments";
			$scope.constants = constants;
			$scope.$location = $location;
			$scope.$route = $route;
			$scope.reload = reload;

			commentsHub.changePageOnHub();

			CommentStorage.query({ page: $scope.page },
			function (data) {
				$scope.comments = data;
				CommentStorage.total(
				function (response) {
					$scope.total = response.total;
				});
			});

			$scope.removeComment = function (comment) {
				comment.$remove()
				.then(function () {
					$scope.comments.splice($scope.comments.indexOf(comment), 1);
					toastr.success(constants["commentDeletedMessage"]);
					$scope.reload($scope, $scope.comments.length, "/" + $scope.pageName + "/{pageNumber}");
				}, function () {
					toastr.error(constants["errorOccurredDuringDeletingProcessMessage"]);
				});
			};
		}]);

