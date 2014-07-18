define(["jquery", "underscore", "Urls"], function ($, _, Urls) {
	var self = {};
	var templates = [];

	self.get = function (key) {
		return _.isUndefined(templates[key]) ? add(key) : templates[key];
	};

	function add(key) {
		$.ajax({
			url: Urls.Template + "?templateName=" + key,
			type: "GET",
			async: false,
			beforeSend: function () {
				$("#preloader").show();
			}
		}).done(function (data) {
			templates[key] = _.template(data);
		}).fail(function () {
			window.router.navigate("page500", { trigger: true });
		}).always(function () {
			$("#preloader").hide();
		});
		return templates[key];
	};

	return self;
});