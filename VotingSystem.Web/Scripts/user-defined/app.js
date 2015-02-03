var votingSystem = angular.module("votingSystem", [
	"ngRoute",
	"ngResource",
	"spinner",
	"votingSystem.urls",
	"votingSystem.constants",
	"votingSystem.controllers",
	"votingSystem.directives",
	"votingSystem.services",
	"votingSystem.unitOfWork"
]);

votingSystem.config(["$routeProvider", "$httpProvider","spinnerProvider","constants", function ($routeProvider, $httpProvider, spinnerProvider, constants) {
	$routeProvider.when("/", {
		redirectTo: "/main/1"
	}).when("/main/:pageNumber/:searchQuery?", {
		templateUrl: "views/templates/main.html",
		controller: "MainCtrl"
	}).when("/userVotings/:pageNumber", {
		templateUrl: "views/templates/userVotings.html"
	}).when("/adminVotings/:pageNumber", {
		templateUrl: "views/templates/adminVotings.html",
	}).when("/users/:pageNumber/:suggested?", {
		templateUrl: "views/templates/users.html"
	}).when("/comments/:pageNumber", {
		templateUrl: "views/templates/comments.html"
	}).when("/voices/:pageNumber", {
		templateUrl: "views/templates/voices.html"
	}).when("/profile/:userId?", {
		templateUrl: "views/templates/userProfile.html",
		controller: "UserProfileCtrl",
		resolve: {
			User: function (UserStorage, $route) {
				return UserStorage.get({ id: $route.current.params.userId}).$promise;
			}
		}
	}).when("/voting/:votingId?", {
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

	spinnerProvider.initialize(constants.spinnerSelector);
	$httpProvider.interceptors.push("myHttpInterceptor");
	$httpProvider.defaults.transformRequest.push(spinnerProvider.showSpinner);
}]).factory("myHttpInterceptor", ["$q", "spinner", function ($q, spinner) {
	return {
		response: function (response) {
			spinner.hideSpinner();
			return response;
		},
		responseError: function (response) {
			spinner.hideSpinner();
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
		}
	};
}]);
