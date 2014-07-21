define(["jquery"],
	function ($) {
		return function (module) {
			module.directive("focusOut", function () {
				return function (scope, elem, attrs) {
					$(elem).focusout(scope[attrs.focusOut]);
				};
			});
		};
	});