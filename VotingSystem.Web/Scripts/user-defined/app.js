define(["jquery", "angular", "angular.route", "angular.resource",
		"services/ServicesModule", "controllers/ControllersModule", "directives/DirectivesModule"
],
	function ($, angular) {

		var votingSystem = angular.module("votingSystem", [
			"ngRoute",
			"ngResource",
			"votingSystem.controllers",
			"votingSystem.directives",
			"votingSystem.services"
		]);

		votingSystem.config(["$routeProvider", "$httpProvider", function ($routeProvider, $httpProvider) {
			$routeProvider.when("/", {
				redirectTo: "/mainpage/1"
			}).when("/mainpage/:pageNumber/:searchQuery?", {
				templateUrl: "views/templates/main.html",
				controller: "MainCtrl"
			}).when("/uservotingspage/:pageNumber", {
				templateUrl: "views/templates/uservotings.html"
			}).when("/adminvotingspage/:pageNumber", {
				templateUrl: "views/templates/adminvotings.html",
			}).when("/userspage/:pageNumber/:suggested?", {
				templateUrl: "views/templates/users.html"
			}).when("/commentspage/:pageNumber", {
				templateUrl: "views/templates/comments.html"
			}).when("/voicespage/:pageNumber", {
				templateUrl: "views/templates/voices.html"
			}).when("/profilepage/:userName?", {
				templateUrl: "views/templates/userprofile.html",
				controller: "UserProfileCtrl",
				resolve: {
					User: function (UserStorage, $route) {
						return UserStorage.get({ userName: $route.current.params.userName }).$promise;
					}
				}
			}).when("/votingpage/:votingId?", {
				templateUrl: "views/templates/voting.html",
				controller: "VotingCtrl",
				resolve: {
					Voting: function (VotingStorage, $route) {
						return VotingStorage.get({ id: $route.current.params.votingId }).$promise;
					}
				}
			}).when("/error:errorType", {
				templateUrl: function (params) {
					return "views/templates/" + params.errorType + ".html";
				}
			}).otherwise({
				redirectTo: "/error404"
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
					switch (response.status) {
						case 404:
							window.location.hash = "#/error404";
							break;
						case 403:
							window.location.hash = "#/error403";
							break;
						default:
							window.location.hash = "#/error500";
					}
					return $q.reject(response);
				});
			};
		});
	});
