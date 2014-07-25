define(["angular", "Urls", "constants", "angular.route"],
	function (angular, Urls, constants) {
		return function (controllersModule) {
			controllersModule
				.controller("MainController", function ($scope, votingStorage, $http, $route, $routeParams) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "mainpage";
					$scope.total = 1;
					$routeParams.searchQuery = $routeParams.searchQuery ? $routeParams.searchQuery : "";
					$scope.searchQuery = $routeParams.searchQuery;
					$scope.constants = constants;

					votingStorage.query(
						{
							pageType: "MainPage",
							page: $routeParams.pageNumber,
							query: $routeParams.searchQuery
						},
						function (data) {
							$scope.votings = data;
							votingStorage.total(
								{
									totalKind: "totalactive",
									query: $routeParams.searchQuery
								},
								function (response) {
									$scope.total = response.total;
								});
						});
				});
		};
	});

