define(["jquery", "markdown"],
	function ($) {
		return function (module) {
			module.directive("trustHtml", function () {
				var converter = new Markdown.getSanitizingConverter();
				return function (scope, elem, attrs) {
					var value = converter.makeHtml(attrs.trustHtml ? attrs.trustHtml : "").substr(0, 1000) + "...";
					$(elem).html(value);
				};
			});
		};
	});