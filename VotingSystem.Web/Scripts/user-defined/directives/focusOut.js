angular.module("votingSystem.directives.focusOut", [])
    .directive("focusOut", function () {
        return {
            scope: { onFocusOut: "&" },
            link: function (scope, elem, attrs) {
                $(elem).focusout(scope.onFocusOut);
            }
        };
    });