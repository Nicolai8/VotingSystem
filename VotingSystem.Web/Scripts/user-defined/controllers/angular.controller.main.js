define(["angular", "Urls", "constants", "markdown", "bootpag", "angular.route"],
	function (angular, Urls, constants) {
		return function (controllersModule) {
			controllersModule
				.controller('MainController', function ($scope, $http, $route, $routeParams) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "mainpage";
					$scope.total = 1;
					$routeParams.searchQuery = $routeParams.searchQuery ? $routeParams.searchQuery : "";
					$scope.searchQuery = $routeParams.searchQuery;
					$scope.converter = new Markdown.getSanitizingConverter();
					$scope.constants = constants;

					$http.get(Urls.Votings + "/MainPage/" + $routeParams.pageNumber + "?query=" + $routeParams.searchQuery).success(function (data) {
						$scope.votings = data;
						$http.get(Urls.MainPage.GetTotal + "?query=" + $routeParams.searchQuery).success(function (total) {
							$scope.total = total;
						});
					});
				});
		};
	});

