define(["angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function (angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller("CommentsController", function ($scope, commentStorage, $reload, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "commentspage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;
					$scope.reload = $reload;

					commentStorage.query({ page: $scope.page },
						function (data) {
							$scope.comments = data;
							commentStorage.total(
								function (response) {
									$scope.total = response.total;
								});
						});

					$scope.removeComment = function(comment) {
						comment.$remove()
							.then(function () {
								$scope.comments.splice($scope.comments.indexOf(comment), 1);
								toastr.success(constants("commentDeletedMessage"));
								$scope.reload($scope, "/" + $scope.pageName + "/{pageNumber}");
							}, function() {
								toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
							});
					};
				});
		};
	});

