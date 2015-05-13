angular.module("votingSystem.services.reloadDataAfterUserAction", [])
	.service("reloadDataAfterUserAction", ["$location", "$route", "$routeParams",
		function ($location, $route, $routeParams) {
			return function (arrayLength, path) {
				var pageNumber = $routeParams.pageNumber;
				if (pageNumber != 1) {
					if (arrayLength == 0) {
						pageNumber -= 1;
					}
				}
				if (pageNumber != $routeParams.pageNumber) {
					$location.path(path.replace("{pageNumber}", pageNumber));
				} else {
					$route.reload();
				}
			};
		}]);