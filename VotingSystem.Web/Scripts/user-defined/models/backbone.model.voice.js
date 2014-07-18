define(["model", "Urls", "toastr", "constants"],
function (Model, Urls, toastr, constants) {
	return Model.extend({
		idAttribute: "VoiceId",

		defaults: {
			AnswerText: "",
			FixedAnswerId: "",
		},

		validate: function (attrs, options) {
			if (attrs.AnswerText.toString().length > 1 || attrs.FixedAnswerId.toString().length > 0) {
				return;
			}
			toastr.info(constants("emptyAnswerMesage"));
			return constants("emptyAnswerMesage");
		},

		methodUrl: {
			"create": function (model) {
				return Urls.Voices + (window.state.get("authenticated") ? "" : "?captcha=" + model.get("Captcha"));
			}
		},

		url: Urls.Voices
	});
});