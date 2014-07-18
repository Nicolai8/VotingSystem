define(["jquery", "underscore", "backbone", "Urls", "voiceModel"],
function ($, _, Backbone, Urls, VoiceModel) {
	return Backbone.Collection.extend({
		model: VoiceModel,
		url: function () {
			return Urls.Voices + "?page=" + window.state.get("page")
				+ "&size=" + window.state.get("pageSize");
		},

		save: function (options) {
			if (this.validate()) {
				this.url = Urls.Voices + (window.state.get("authenticated") ? "" : "?captcha=" + this.captcha);
				return Backbone.sync("create", this, options);
			}
		},
		validate: function () {
			var validationResult;
			var voiceModel = new VoiceModel();
			$.each(this.toJSON(), function (i, val) {
				validationResult = voiceModel.validate(val);
				if (!_.isUndefined(validationResult)) {
					return false;
				}
			});
			return _.isUndefined(validationResult);
		}
	});
});