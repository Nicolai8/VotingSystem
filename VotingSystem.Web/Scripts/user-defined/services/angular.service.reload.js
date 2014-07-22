define([],
	function () {
		return function (module) {
			module.factory("$reload", function () {
				return function (scope, path) {
					var pageNumber = scope.page;
					if (pageNumber != 1) {
						if (scope.votings.length == 0) {
							pageNumber -= 1;
						}
					}
					if (pageNumber != scope.page) {
						scope.$location.path(path.replace("{pageNumber}", pageNumber));
					} else {
						scope.$route.reload();
					}
				};
			});
		};
	});

