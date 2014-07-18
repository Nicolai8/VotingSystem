define(["jquery", "underscore", "pageCollection", "votingView", "votingsCollection", "Urls", "toastr", "constants", "templates", "markdown", "bootpag"],
	function ($, _, PageCollection, VotingView, VotingsCollection, Urls, toastr, constants, templates) {
		var votingView = VotingView.extend({
			tagName: "div",
			className: "mainThumbnail margin-bottom-x2",

			templateName: "MainPageVotingTemplate",

			initialize: function (model) {
				this.model = model;
				var converter = new Markdown.getSanitizingConverter();
				var description = this.model.get("Description");
				this.model.set("Description", converter.makeHtml(description ? description : ""));
			},
		});

		var mainPageView = PageCollection.extend({
			el: $("#mainPage"),
			totalUrl: Urls.MainPage.GetTotal,

			appendTheme: function (theme) {
				theme.modelView = new votingView(theme);
				$(this.el).find("#mainPageVotingsContent").append(theme.modelView.render().el);
			},
			initialize: function () {
				this.collection = new VotingsCollection();
				this.objectToFetch = this.collection;
				this.collection.pageType = "MainPage";
				window.state.on("change:pageName=mainpage", this.render, this);
			},

			fillData: function () {
				if (this.collection.length > 0) {
					this.collection.each(this.appendTheme, this);
				} else {
					$("#dataNotFoundWithSideBar").append(templates.get("NoDataTemplate")({ Message: constants("votingsNotFoundMessage") }));
				}
				this.updatePaginator();
				$("#withSidebar").show();
				$(this.el).addClass("open-page").show();
			},
		});

		return mainPageView;
	});