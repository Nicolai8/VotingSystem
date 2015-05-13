angular.module("votingSystem.directives.cleanUp", [])
	.directive("cleanUp", function() {
	return function(scope, elem, attrs) {
		angular.element(elem).on("hidden.bs.modal", "[role='dialog']", function(e) {
			var $modal = $(e.currentTarget);
			$modal.find(":text, textarea, :password").each(function() {
				$(this).val("");
			});
			$modal.find(":checked").each(function() {
				$(this).attr("checked", "false");
			});
			$modal.find(".wmd-preview").empty();
			$modal.find("form[data-validate-form]").each(function() {
				var validateForm = $(this).data("bootstrapValidator");
				if (angular.isDefined(validateForm)) {
					validateForm.resetForm();
				}
			});
		});
	}
});