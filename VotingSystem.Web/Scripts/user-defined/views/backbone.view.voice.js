define(["jquery", "underscore", "view"],
	function ($, _, View) {
		var voiceView = View.extend({
			templateName: "VoiceTemplate",
		});
		return voiceView;
	});