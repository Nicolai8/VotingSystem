angular.module("votingSystem.directives.validateForm", [])
    .directive("validateForm", function () {
        return function (scope, elem, attrs) {
            if (attrs.validateForm != "false") {
                $(elem).bootstrapValidator({
                    message: 'This field is required',
                    feedbackIcons: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    },
                    excluded: [':disabled'],
                });
            }
        };
    });