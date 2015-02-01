angular.module("spinner", [])
	.provider('spinner', function providerConstructor() {
		this.initialize = function () {
			//REVIEW: global for all module constant
			var opts = {
				lines: 10,
				length: 15,
				width: 8,
				radius: 20,
				trail: 60
			};
			//REVIEW: unused local variable
			//REVIEW: "#preloader" should be constant
			var spinner = new Spinner(opts).spin($("#preloader")[0]);
		};

		this.showSpinner = function (data) {
			$("#preloader").show();
			return data;
		};

		this.$get = function () {
			return {
					hideSpinner:function() {
						$("#preloader").hide();
					}
				}
		}
	});
