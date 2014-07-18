define(["model", "toastr", "constants"],
function (Model, toastr, constants) {
	return Model.extend({
		idAttribute: "QuestionId",

		defaults: {
			"Type": 0
		},

		validate: function (attrs, options) {
			if (options.skipValidation) {
				return;
			}
			if (attrs.Text.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Question Text"));
				return constants("votingCreateValidateMessage").replace("{0}", "Question Text");
			}
			if (attrs.Type == 1 && (typeof attrs.FixedAnswers === "undefined" || attrs.FixedAnswers.length < 2)) {
				toastr.info(constants("votingCreateValidateTypeMessage"));
				return constants("votingCreateValidateTypeMessage");
			}
		},
	});
});