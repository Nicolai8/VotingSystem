define(["jquery", "angular"],
	function ($, angular) {
		angular.module("votingSystem.directives.focusOut", [])
			.directive("focusOut", function () {
				return {
					scope: {onFocusOut:"&onFocusOut"},
					link: function(scope, elem, attrs) {
						$(elem).focusout(scope.onFocusOut);
					}
				};
			});
	});