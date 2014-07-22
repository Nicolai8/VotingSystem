define(["jquery", "angular", "controllers/angular.controller.layout", "controllers/angular.controller.main"
	, "controllers/angular.controller.adminvotings"
	, "directives/angular.directive.paginator", "directives/angular.directive.validateform"
	, "directives/angular.directive.focusout"
	, "services/angular.service.votingstorage"
	, "angular.route"],
	function ($, angular, layoutController, mainController, adminVotingsController,
		paginatorDirective, validateFormDirective, focusOutDirective, votingStorageService) {
		var votingsystemControllers = angular.module('votingsystemControllers', []);
		layoutController(votingsystemControllers);
		mainController(votingsystemControllers);
		adminVotingsController(votingsystemControllers);

		var votingSystem = angular.module('votingSystem', [
			'ngRoute',
			"ngResource",
			'votingsystemControllers'
		]);

		votingStorageService(votingSystem);

		votingSystem.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
			$routeProvider.when('/mainpage/:pageNumber', {
				controller: 'MainController',
				templateUrl: "static/main.html",
			}).when('/mainpage/:pageNumber/:searchQuery', {
				controller: 'MainController',
				templateUrl: "static/main.html",
			}).when('/adminvotingspage/:pageNumber', {
				controller: 'AdminVotingsController',
				templateUrl: "static/adminvotings.html",
			}).
			otherwise({
				redirectTo: '/mainpage/1'
			});
			$httpProvider.responseInterceptors.push('myHttpInterceptor');
			var spinnerFunction = function (data) {
				$("#preloader").show();
				return data;
			};
			$httpProvider.defaults.transformRequest.push(spinnerFunction);
		}]).factory('myHttpInterceptor', function ($q) {
			return function (promise) {
				return promise.then(function (response) {
					$("#preloader").hide();
					return response;

				}, function (response) {
					$("#preloader").hide();
					return $q.reject(response);
				});
			};
		});

		paginatorDirective(votingSystem);
		validateFormDirective(votingSystem);
		focusOutDirective(votingSystem);
	});
