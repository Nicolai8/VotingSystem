define(["jquery", "angular"],
	function ($, angular) {
		angular.module("votingSystem.directives.onEnterPress", [])
			.directive("onEnterPress", function () {
				return function (scope, elem, attrs) {
					$(elem).keypress(function (e) {
						if (e.which == 13 || e.which == 10) {
							if (angular.isDefined(attrs.onEnterPress) && attrs.onEnterPress.length > 0) {
								scope[attrs.onEnterPress]();
								return;
							}
							if (angular.isDefined(attrs.target) && attrs.target.length > 0) {
								$(attrs.target)[0].click();
								return;
							}
						}
					});
				};
			});
	});