define(["jquery", "angular", "angular.route", "angular.resource",
		"services/angular.services", "controllers/angular.controllers", "directives/angular.directives"
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
			}).when("/error:errorType", {
				templateUrl: function (params) {
					return "static/" + params.errorType + ".html";
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
