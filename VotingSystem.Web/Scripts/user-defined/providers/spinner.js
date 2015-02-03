angular.module("spinner", [])
	.provider('spinner', function providerConstructor() {
		var that = this;

		this.initialize = function (spinnerSelector) {
			that.$spinner = angular.element(spinnerSelector);

			new Spinner({
				lines: 10,
				length: 15,
				width: 8,
				radius: 20,
				trail: 60
			}).spin(that.$spinner[0]);
		};

		this.showSpinner = function (data) {
			that.$spinner.show();
			return data;
		};

		this.$get = function () {
			return {
				hideSpinner: function () {
					that.$spinner.hide();
				}
			}
		}
	});
