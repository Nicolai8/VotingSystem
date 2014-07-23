define(["jquery", "angular", "controllers/angular.controller.layout", "controllers/angular.controller.main"
	, "controllers/angular.controller.adminvotings", "controllers/angular.controller.users"
	, "controllers/angular.controller.comments"
	, "directives/angular.directive.paginator", "directives/angular.directive.validateform"
	, "directives/angular.directive.focusout"
	, "services/angular.service.votingstorage", "services/angular.service.userstorage"
	, "services/angular.service.reload", "services/angular.service.commentstorage"
	, "angular.route"],
	function ($, angular,
		layoutController, mainController, adminVotingsController, usersController, commentsController,
		paginatorDirective, validateFormDirective, focusOutDirective, 
		votingStorageService, userStorageService, reloadService, commentStorageService) {
		
		var votingsystemControllers = angular.module('votingsystemControllers', []);
		
		layoutController(votingsystemControllers);
		mainController(votingsystemControllers);
		adminVotingsController(votingsystemControllers);
		usersController(votingsystemControllers);
		commentsController(votingsystemControllers);

		var votingSystem = angular.module('votingSystem', [
			'ngRoute',
			"ngResource",
			'votingsystemControllers'
		]);

		votingStorageService(votingSystem);
		userStorageService(votingSystem);
		reloadService(votingSystem);
		commentStorageService(votingSystem);

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
			}).when("/userspage/:pageNumber/:suggested?", {
				controller: "UsersController",
				templateUrl: "static/users.html"
			}).when("/commentspage/:pageNumber", {
				controller: "CommentsController",
				templateUrl: "static/comments.html"
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
