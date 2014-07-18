define(["jquery", "underscore", "pageCollection", "votingsCollection", "votingView", "Urls", "toastr",
		"constants", "templates", "bootpag"],
	function ($, _, PageCollection, VotingsCollection, VotingView, Urls, constants, templates) {
		var votingView = VotingView.extend({
			templateName: "AdminVotingsPageVotingTemplate"
		});

		var adminVotingsPageView = PageCollection.extend({
			el: $("#adminVotingsPage"),
			breadCrumbItemName: "Administrate Votings",
			totalUrl: Urls.AdminPage.GetTotal,

			appendTheme: function (theme) {
				theme.modelView = new votingView(theme);
				$(this.el).find("#adminVotingsPageVotingsContent").append(theme.modelView.render().el);
			},
			initialize: function () {
				this.collection = new VotingsCollection();
				this.objectToFetch = this.collection;
				this.collection.pageType = "AdminVotings";
				this.collection.on("destroy", this.prerender, this);
				window.state.on("change:pageName=adminvotingspage", this.render, this);
			},

			fillData: function () {
				if (this.collection.length > 0) {
					this.$el.find("#adminVotingsPageVotingsContent").closest("table").show();
					this.collection.each(this.appendTheme, this);
				} else {
					this.$el.find("#adminVotingsPageVotingsContent").closest("table").hide();
					$("#dataNotFound").append(templates.get("NoDataTemplate")({ Message: constants("votingsNotFoundMessage") }));
				}
				this.updatePaginator();
			},
		});

		return adminVotingsPageView;
	});