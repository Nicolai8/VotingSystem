define(["angular", "bootstrap", "markdown"],
	function (angular) {
		angular.module("votingSystem.directives.bootstrapMarkdown", [])
			.directive("bootstrapMarkdown", function () {
				return function (scope, elem, attrs) {
					angular.element(elem).pagedownBootstrap();
				};
			});
	});