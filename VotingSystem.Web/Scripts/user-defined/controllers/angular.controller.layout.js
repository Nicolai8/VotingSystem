define(["angular", "angular.route"], function () {
	return function (controllersModule) {
		controllersModule
			.controller('LayoutController', function ($scope, $route, $routeParams, $location) {
				$scope.accountName = "Account";
			});
	};
});


