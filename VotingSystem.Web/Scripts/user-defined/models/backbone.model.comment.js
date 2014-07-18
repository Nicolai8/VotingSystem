define(["model", "Urls", "toastr", "constants"],
function (Model, Urls, toastr, constants) {
	return Model.extend({
		idAttribute: "CommentId",

		validate: function (attrs, options) {
			if (attrs.CommentText.length < 4) {
				toastr.info(constants("commentLengthMessage"));
				return constants("commentLengthMessage");
			}
		},

		urlRoot: Urls.Comments
	});
});