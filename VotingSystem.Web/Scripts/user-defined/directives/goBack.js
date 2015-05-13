angular.module("votingSystem.directives.goBack", [])
	.directive("goBack", function() {
		return function(scope, elem, attrs) {
			elem.on("click", function() {
				window.history.back();
			});
		};
	});