angular.module("votingSystem.directives.votingPaginator", [])
    .directive("votingPaginator", function () {
        return function (scope, elem, attrs) {
            attrs.$observe("total", function () {
                if (scope.total == 0) {
                    scope.total = 1;
                }
                var pageSize = 10;
                var totalPages = Math.floor((scope.total - 1) / pageSize) + 1;
                $(elem).bootpag({
                    href: "#" + scope.pageName + "/{{number}}",
                    total: totalPages,
                    maxVisible: 10,
                    page: scope.page
                });
            });
        };
    });