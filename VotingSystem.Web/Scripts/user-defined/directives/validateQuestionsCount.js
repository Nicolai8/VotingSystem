define(["jquery", "angular"],
	function ($, angular) {
		

		var INTEGER_REGEXP = /^\-?\d+$/;
		angular.module("votingSystem.directives.integer", [])
			.directive('integer', function () {
			return {
				require: 'ngModel',
				link: function (scope, elm, attrs, ctrl) {
					ctrl.$validators.integer = function (modelValue, viewValue) {
						if (ctrl.$isEmpty(modelValue)) {
							// consider empty models to be valid
							return true;
						}

						if (INTEGER_REGEXP.test(viewValue)) {
							// it is valid
							return true;
						}

						// it is invalid
						return false;
					};
				}
			};
		});


		//angular.module("votingSystem.directives.validatequestionscount", [])
		//	.directive("validatequestionscount", function () {
		//		return {
		//			require: 'ngModel',
		//			link: function (scope, elm, attrs, ctrl) {
		//				ctrl.$validators.validateQuestionsCount = function (modelValue, viewValue) {
		//					if (ctrl.$isEmpty(modelValue)) {
		//						return true;
		//					}
							
		//					return false;
		//				};
		//			}
		//		};
		//	});
	});