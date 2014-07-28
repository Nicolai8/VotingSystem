﻿define(["jquery", "angular", "constants"],
	function ($, angular, constants) {
		angular.module("votingSystem.directives.notFound", [])
			.directive("notFound", function () {
				return {
					templateUrl: "static/notfound.html",
					link: function (scope, element, attrs) {
						scope.notFoundMessage = constants(attrs.notFound);
					}
				};
			});
	});