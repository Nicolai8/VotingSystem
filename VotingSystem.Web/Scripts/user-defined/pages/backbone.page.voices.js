define(["jquery", "underscore", "pageCollection", "voicesCollection", "voiceView", "Urls", "constants", "templates", "bootpag"],
	function ($, _, PageCollection, VoicesCollection, VoiceView, Urls, constants, templates) {

		var myVoicesPageView = PageCollection.extend({
			el: $("#voicesPage"),
			breadCrumbItemName: "Voices",
			totalUrl: Urls.VoicesPage.GetTotal,

			initialize: function () {
				this.collection = new VoicesCollection();
				this.objectToFetch = this.collection;
				window.state.on("change:pageName=myvoicespage", this.render, this);
			},

			fillData: function () {
				if (this.collection.length > 0) {
					var voices = this.$el.find("#voices");
					this.collection.each(function (voice) {
						voice.modelView = new VoiceView(voice);
						voices.append(voice.modelView.render().el);
					}, this);
				} else {
					$("#dataNotFound").append(templates.get("NoDataTemplate")({ Message: constants("dontHaveVoicesMessage") }));
				}
				this.updatePaginator();
			},
		});

		return myVoicesPageView;
	})