angular.module("votingSystem.directives.dateTimePicker", [])
	.directive("dateTimePicker", function() {
		return function(scope, elem, attrs) {
			var $elem = $(elem);
			var scopeCopy = scope;

			var options = {
				validate: attrs.validate == "true",
				validateField: $elem.find(":text").attr("name"),
				isDepend: attrs.isDepend == "true",
				dependOn: attrs.dependOn,
				dependOnMin: attrs.dependOnMin == "true",
				onChange: attrs.onChange,
				onChangeProperty: attrs.onChangeProperty,
				format: $elem.find(":text").data("format")
			};

			$elem.datetimepicker({
				pickTime: false
			}).on("dp.change dp.show", function(e) {
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

				if (angular.isDefined(options.onChange) && options.onChange.length > 0) {
					scopeCopy[options.onChange](options.onChangeProperty, e.date.format(options.format));
				}
			});
		};
	});