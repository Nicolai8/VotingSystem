angular.module("spinner", []).provider('spinner', function providerConstructor() {
	this.initialize = function() {
		var opts = {
			lines: 10,
			length: 15,
			width: 8,
			radius: 20,
			trail: 60
		};
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
