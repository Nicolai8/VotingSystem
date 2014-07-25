define(["angular", "bootstrap", "markdown"],
	function (angular) {
		return function (module) {
			module.directive("bootstrapMarkdown", function () {
				return function (scope, elem, attrs) {
					angular.element(elem).pagedownBootstrap();
				};
			});
		};
	});