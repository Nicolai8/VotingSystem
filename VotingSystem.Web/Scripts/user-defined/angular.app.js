define(["jquery", "angular", "controllers/angular.controller.layout", "controllers/angular.controller.main"
	, "controllers/angular.controller.adminvotings", "controllers/angular.controller.users"
	, "controllers/angular.controller.comments", "controllers/angular.controller.voices"
	, "controllers/angular.controller.userprofile", "controllers/angular.controller.voting"
	, "controllers/angular.controller.uservotings"
	, "directives/angular.directive.paginator", "directives/angular.directive.validateform"
	, "directives/angular.directive.focusout", "directives/angular.directive.goback"
	, "directives/angular.directive.piechart", "directives/angular.directive.bootstrapmarkdown"
	, "directives/angular.directive.datetimepicker", "directives/angular.directive.trusthtml"
	, "directives/angular.directive.breadcrumb"
	, "services/angular.service.votingstorage", "services/angular.service.userstorage"
	, "services/angular.service.reload", "services/angular.service.commentstorage"
	, "services/angular.service.voicestorage"
	, "angular.route"//, "goog!visualization,1,packages:[corechart]"
],
	function ($, angular,
		layoutController, mainController, adminVotingsController, usersController,
		commentsController, voicesController, userProfileController, votingController,
		userVotingsController,
		paginatorDirective, validateFormDirective, focusOutDirective, goBackDirective,
		pieChartDirective, bootstrapMarkdownDirective, dateTimePickerDirective, trustHtmlDirective,
		breadCrumbDirective,
		votingStorageService, userStorageService, reloadService, commentStorageService,
		voiceStorageService) {

		var votingsystemControllers = angular.module("votingsystemControllers", []);

		layoutController(votingsystemControllers);
		mainController(votingsystemControllers);
		adminVotingsController(votingsystemControllers);
		usersController(votingsystemControllers);
		commentsController(votingsystemControllers);
		voicesController(votingsystemControllers);
		userProfileController(votingsystemControllers);
		votingController(votingsystemControllers);
		userVotingsController(votingsystemControllers);

		var votingSystem = angular.module("votingSystem", [
			"ngRoute",
			"ngResource",
			"votingsystemControllers"
		]);

		votingStorageService(votingSystem);
		userStorageService(votingSystem);
		reloadService(votingSystem);
		commentStorageService(votingSystem);
		voiceStorageService(votingSystem);

		votingSystem.config(["$routeProvider", "$httpProvider", function ($routeProvider, $httpProvider) {
			$routeProvider.when("/mainpage/:pageNumber/:searchQuery?", {
				templateUrl: "static/main.html",
				controller: "MainController"
			}).when("/uservotingspage/:pageNumber", {
				templateUrl: "static/uservotings.html"
			}).when("/adminvotingspage/:pageNumber", {
				templateUrl: "static/adminvotings.html",
			}).when("/userspage/:pageNumber/:suggested?", {
				templateUrl: "static/users.html"
			}).when("/commentspage/:pageNumber", {
				templateUrl: "static/comments.html"
			}).when("/voicespage/:pageNumber", {
				templateUrl: "static/voices.html"
			}).when("/profilepage/:userName?", {
				templateUrl: "static/userprofile.html",
				controller: "UserProfileController",
				resolve: {
					user: function (userStorage, $route) {
						return userStorage.get({ userName: $route.current.params.userName }).$promise;
					}
				}
			}).when("/votingpage/:votingId?", {
				templateUrl: "static/voting.html",
				controller: "VotingController",
				resolve: {
					voting: function (votingStorage, $route) {
						return votingStorage.get({ id: $route.current.params.votingId }).$promise;
					}
				}
			}).otherwise({
				redirectTo: "/mainpage/1"
			});

			$httpProvider.responseInterceptors.push("myHttpInterceptor");
			var spinnerFunction = function (data) {
				$("#preloader").show();
				return data;
			};
			$httpProvider.defaults.transformRequest.push(spinnerFunction);
		}]).factory("myHttpInterceptor", function ($q) {
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
		goBackDirective(votingSystem);
		pieChartDirective(votingSystem);
		bootstrapMarkdownDirective(votingSystem);
		dateTimePickerDirective(votingSystem);
		trustHtmlDirective(votingSystem);
		breadCrumbDirective(votingSystem);
	});
