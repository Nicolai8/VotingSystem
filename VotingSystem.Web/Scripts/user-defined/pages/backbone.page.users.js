define(["jquery", "underscore", "pageCollection", "usersCollection", "userView", "Urls", "toastr", "constants", "templates", "bootpag"],
	function ($, _, PageCollection, UsersCollection, UserView, Urls, toastr, constants, templates) {
		var usersPageView = PageCollection.extend({
			el: $("#usersPage"),

			events: {
				"click #changeRoles": "changeRoles"
			},

			initialize: function () {
				this.isSuggested = arguments[0].type == "suggesteduserspage";
				this.collection = new UsersCollection();
				this.objectToFetch = this.collection;
				if (this.isSuggested) {
					this.collection.pageType = "Suggested Users";
				} else {
					this.collection.pageType = "Users";
				}
				this.breadCrumbItemName = "Administrate " + this.collection.pageType;
				this.collection.CollectionView = this;
				window.state.on("change:pageName=" + arguments[0].type, this.render, this);
				this.collection.on("destroy", this.prerender, this);
			},

			fillData: function () {
				var usersTag = this.$el.find("#usersPageContent");
				usersTag.empty();
				var that = this;
				if (this.collection.length > 0) {
					usersTag.closest("table").show();
					this.collection.each(function (user) {
						user.set("IsSuggested", that.isSuggested);
						user.modelView = new UserView(user);
						usersTag.append(user.modelView.render().el);
					});
				} else {
					usersTag.closest("table").hide();
					$("#dataNotFound").append(templates.get("NoDataTemplate")({ Message: constants("usersNotFoundMessage") }));
				}
				this.updatePaginator();
			},
			getTotalUrl: function () {
				if (this.isSuggested) {
					return Urls.UsersPage.GetTotalSuggestedUsers;
				}
				return Urls.UsersPage.GetTotal;
			},
			changeRoles: function () {
				if (!this.isSuggested) {
					var that = this;
					var $roles = this.$el.find("#roles");
					var roles = [];
					$.each($roles.find("input:checkbox:checked"), function (index, value) {
						roles[roles.length] = value.value;
					});
					var userName = $roles.attr("userName");
					this.collection.findWhere({ UserName: userName })
						.save({ "Roles": roles },
							{
								wait: true,
								success: function () {
									that.$el.find("#changeRolesModal").modal("hide");
									toastr.success(constants("userRolesChangedMessage"));
								},
								error: function () {
									toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
								}
							});
				}
			},
		});

		return usersPageView;
	})