define(["model", "Urls", "toastr", "constants"],
function (Model, Urls, toastr, constants) {
	return Model.extend({
		idAttribute: "Id",

		validate: function(attrs, options) {
			if (attrs.AnswerText < 1) {
				return constants("emptyFixedAnswerMessage");
			}
		}
	});
});