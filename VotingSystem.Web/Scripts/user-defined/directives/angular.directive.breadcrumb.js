define([], function () {
	return function (module) {
		module.directive("breadcrumb", function () {
			return {
				templateUrl: "static/breadcrumb.html"
			};
		});
	};
});