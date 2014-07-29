define(["jquery", "angular", "bootstrap", "bootstrapDatePicker"],
	function ($, angular) {
		angular.module("votingSystem.directives.datetimePicker", [])
			.directive("datetimePicker", function () {
				return function (scope, elem, attrs) {
					var $elem = $(elem);

					var options = {
						validate: attrs.validate == "true",
						validateField: $elem.find("input").attr("name"),
						isDepend: attrs.isDepend == "true",
						dependOn: attrs.dependOn,
						dependOnMin: attrs.dependOnMin == "true"
					};

					$elem.datetimepicker({
						pickTime: false
					}).on("dp.change dp.show", function (e) {
						if (options.validate) {
							$elem.closest("form[data-validate-form='']")
								.data("bootstrapValidator")
								.updateStatus(options.validateField, "NOT_VALIDATED", null)
								.validateField(options.validateField);
						}
						if (options.isDepend)
							if (options.dependOnMin) {
								$(options.dependOn).data("DateTimePicker").setMinDate(e.date);
							} else {
								$(options.dependOn).data("DateTimePicker").setMaxDate(e.date);
							}
					});
				};
			});
	});