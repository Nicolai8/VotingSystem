angular.module("votingSystem.directives.notFound", [])
	.directive("notFound", [
		"constants",
		function(constants) {
			return {
				templateUrl: "views/templates/notfound.html",
				scope: {},
				link: function(scope, element, attrs) {
					scope.notFoundMessage = constants[attrs.notFound];
				}
			};
		}
	]);