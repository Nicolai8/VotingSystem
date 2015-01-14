angular.module("votingSystem.services.reload", [])
    .service("reload", function () {
        return function (scope, arrayLength, path) {
            var pageNumber = scope.page;
            if (pageNumber != 1) {
                if (arrayLength == 0) {
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