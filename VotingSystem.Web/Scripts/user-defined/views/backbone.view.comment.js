define(["jquery", "underscore", "backbone", "view", "Urls", "toastr", "constants"],
	function ($, _, Backbone, View, Urls, toastr, constants) {
		var commentView = View.extend({
			className: "col-md-12 comment",

			templateName: "CommentTemplateForCommentsPage",

			events: {
				"click .remove-comment-button": "removeComment",
			},

			removeComment: function (e) {
				e.preventDefault();
				this.model.destroy({ wait: true })
					.done(function () {
						toastr.success(constants("commentDeletedMessage"));
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
					});
			}
		});
		return commentView;
	});
