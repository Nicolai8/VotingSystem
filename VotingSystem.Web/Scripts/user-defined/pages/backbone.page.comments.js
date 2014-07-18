define(["jquery", "underscore", "pageCollection", "commentsCollection", "commentView", "Urls", "toastr", "constants", "templates", "bootpag"],
	function ($, _, PageCollection, CommentsCollection, CommentView, Urls, toastr, constants, templates) {
		var myCommentsPageView = PageCollection.extend({
			el: $("#commentsPage"),
			breadCrumbItemName: "Comments",
			totalUrl: Urls.CommentsPage.GetTotal,

			initialize: function () {
				this.collection = new CommentsCollection();
				this.objectToFetch = this.collection;
				this.collection.on("destroy", this.prerender, this);
				window.state.on("change:pageName=mycommentspage", this.render, this);
			},

			fillData: function () {
				if (this.collection.length > 0) {
					var comments = this.$el.find("#comments");
					this.collection.each(function (comment) {
						comment.modelView = new CommentView(comment);
						comments.append(comment.modelView.render().el);
					}, this);
				} else {
					$("#dataNotFound").append(templates.get("NoDataTemplate")({ Message: constants("dontHaveCommentsMessage") }));
				}
				this.$el.show();
				this.updatePaginator();
			}
		});

		return myCommentsPageView;
	})