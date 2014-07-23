define([], function () {
	return function (module) {
		module.directive("goBack", function () {
			return function (scope, elem, attrs) {
				elem.on("click", function () {
					window.history.back();
				});
			};
		});
	};
});