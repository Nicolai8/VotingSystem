define(["angular", "Urls", "constants", "angular.route"],
	function (angular, Urls, constants) {
		angular.module("votingSystem.controllers.voices", [])
			.controller("VoicesCtrl", function ($scope, $http, $route, $routeParams, $location, reload, VoiceStorage) {
				$scope.page = $routeParams.pageNumber;
				$scope.pageName = "voicespage";
				$scope.total = 1;
				$scope.constants = constants;
				$scope.$location = $location;
				$scope.$route = $route;
				$scope.reload = reload;

				$scope.$parent.changePageOnHub();

				VoiceStorage.query({ page: $scope.page },
					function (voices) {
						$scope.voices = voices;
						VoiceStorage.total(
							function (response) {
								$scope.total = response.total;
							});
					});
			});
	});

