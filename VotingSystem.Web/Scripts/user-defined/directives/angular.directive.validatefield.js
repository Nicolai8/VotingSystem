define(["jquery", "angular", "bootstrapValidator"],
	function ($, angular) {
		angular.module("votingSystem.directives.validateField", [])
			.directive("validateField", function () {
				return function (scope, elem, attrs) {
					var $elem = $(elem);
					if (angular.isDefined(attrs.validateField) && attrs.validateField.length > 0) {
						$elem.attr("name", attrs.validateField);
					}
					var $form = $elem.closest("form[data-validate-form]");
					$form.bootstrapValidator("addField", $elem);
				};
			});
	});