define(["jquery", "angular"],
	function ($, angular) {
		angular.module("votingSystem.directives.focusOut", [])
			.directive("focusOut", function () {
				return function (scope, elem, attrs) {
					$(elem).focusout(scope[attrs.focusOut]);
				};
			});
	});