define(["angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function (angular, Urls, constants, toastr) {
		angular.module("votingSystem.controllers.comments", [])
			.controller("CommentsCtrl", function ($scope, $http, $route, $routeParams, $location, reload, CommentStorage) {
				$scope.page = $routeParams.pageNumber;
				$scope.pageName = "commentspage";
				$scope.total = 1;
				$scope.constants = constants;
				$scope.$location = $location;
				$scope.$route = $route;
				$scope.reload = reload;

				$scope.$parent.changePageOnHub();

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
							toastr.success(constants("commentDeletedMessage"));
							$scope.reload($scope, $scope.comments.length, "/" + $scope.pageName + "/{pageNumber}");
						}, function () {
							toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
						});
				};
			});
	});

