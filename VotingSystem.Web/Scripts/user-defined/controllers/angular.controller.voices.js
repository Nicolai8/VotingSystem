define(["angular", "Urls", "constants", "angular.route"],
	function (angular, Urls, constants) {
		return function (controllersModule) {
			controllersModule
				.controller("VoicesController", function ($scope, voiceStorage, $reload, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "voicespage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;
					$scope.reload = $reload;
					
					$scope.$parent.changePageOnHub();

					voiceStorage.query({ page: $scope.page },
						function (voices) {
							$scope.voices = voices;
							voiceStorage.total(
								function (response) {
									$scope.total = response.total;
								});
						});
				});
		};
	});

