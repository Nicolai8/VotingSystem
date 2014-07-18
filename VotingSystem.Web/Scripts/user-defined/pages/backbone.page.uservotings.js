define(["jquery", "underscore", "pageCollection", "addVotingView", "votingModel", "votingsCollection", "votingView",
		"Urls", "toastr", "constants", "templates", "bootstrapDatePicker", "markdown", "bootpag", "stickit"],
	function ($, _, PageCollection, AddVotingView, VotingModel, VotingsCollection, VotingView,
		Urls, toastr, constants, templates) {
		var votingView = VotingView.extend({
			templateName: "MyVotingsPageVotingTemplate"
		});

		var userVotingsPageView = PageCollection.extend({
			el: $("#userVotingsPage"),
			breadCrumbItemName: "My Votings",
			totalUrl: Urls.MyVotingsPage.GetTotal,

			events: {
				"show.bs.modal #addNewVotingModal": "renderNewVotingModal",
				"hide.bs.modal #addNewVotingModal": "unrenderNewVotingModal",
			},

			appendTheme: function (theme) {
				theme.modelView = new votingView(theme);
				this.$el.find("#myVotingsPageVotingsContent").append(theme.modelView.render().el);
			},
			initialize: function () {
				this.collection = new VotingsCollection();
				this.objectToFetch = this.collection;
				this.collection.pageType = "UserVotings";
				this.collection.on("destroy", this.prerender, this);
				window.state.on("change:pageName=myvotingspage", this.render, this);

				$("#newVotingDescription").pagedownBootstrap();
				this.initializeDateTimePicker();
			},
			initializeDateTimePicker: function () {
				$('#newVotingStartDate').datetimepicker({
					pickTime: false
				}).on('dp.change dp.show', function (e) {
					$("#addNewVotingModal form[data-validate-form='true']")
						.data('bootstrapValidator')
						.updateStatus('StartDate', 'NOT_VALIDATED', null)
						.validateField('StartDate');
					$('#newVotingFinishDate').data("DateTimePicker").setMinDate(e.date);
				});
				$('#newVotingFinishDate').datetimepicker({
					pickTime: false
				}).on('dp.change dp.show', function (e) {
					$("#addNewVotingModal form[data-validate-form='true']")
						.data('bootstrapValidator')
						.updateStatus('FinishDate', 'NOT_VALIDATED', null)
						.validateField('FinishDate');
					$('#newVotingStartDate').data("DateTimePicker").setMaxDate(e.date);
				});
			},

			fillData: function () {
				if (this.collection.length > 0) {
					this.$el.find("#myVotingsPageVotingsContent").closest("table").show();
					this.collection.each(this.appendTheme, this);
				} else {
					this.$el.find("#myVotingsPageVotingsContent").closest("table").hide();
					$("#dataNotFound").append(templates.get("NoDataTemplate")({ Message: constants("dontHaveVotingsMessage") }));
				}
				this.updatePaginator();
			},

			renderNewVotingModal: function () {
				this.addVotingView = new AddVotingView();
			},
			unrenderNewVotingModal: function () {
				if (!this.addVotingView.cancel) {
					this.prerender();
				}
				this.addVotingView.unrender();
			},
		});

		return userVotingsPageView;
	});