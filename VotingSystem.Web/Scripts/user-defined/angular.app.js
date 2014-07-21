define(["angular", "controllers/angular.controller.layout", "controllers/angular.controller.main"
	, "directives/angular.directive.paginator", "directives/angular.directive.validateform"
	, "directives/angular.directive.focusout"
	, "angular.route"],
	function (angular, layoutController, mainController, paginatorDirective, validateFormDirective, focusOutDirective) {
		var votingsystemControllers = angular.module('votingsystemControllers', []);
		layoutController(votingsystemControllers);
		mainController(votingsystemControllers);

		var votingSystem = angular.module('votingSystem', [
			'ngRoute',
			'votingsystemControllers'
		]);

		votingSystem.config(['$routeProvider', function ($routeProvider) {
			$routeProvider.when('/mainpage/:pageNumber', {
				controller: 'MainController',
				templateUrl: "static/main.html",
			}).when('/mainpage/:pageNumber/:searchQuery', {
				controller: 'MainController',
				templateUrl: "static/main.html",
			}).
			otherwise({
				redirectTo: '/mainpage/1'
			});
		}]);

		paginatorDirective(votingSystem);
		validateFormDirective(votingSystem);
		focusOutDirective(votingSystem);
	});
