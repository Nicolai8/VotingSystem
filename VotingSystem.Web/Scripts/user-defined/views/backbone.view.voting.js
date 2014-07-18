define(["jquery", "underscore", "view", "Urls", "toastr", "constants"],
	function ($, _, View, Urls, toastr, constants) {
		var votingView = View.extend({
			tagName: "tr",
			className: "",

			events: {
				"click .remove-theme-button": "removeTheme",
				"click .set-theme-status-button": "setThemeStatus",
			},

			defineCustomBind: function () {
				this.model.on("change:Status", this.render, this);
			},

			setThemeStatus: function (e) {
				e.preventDefault();
				this.model.save({ "Status": $(e.currentTarget).attr("status") }, { wait: true, skipValidation: true })
					.done(function () {
						toastr.success(constants("votingStatusChangedMessage"));
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					});
			},
			removeTheme: function (e) {
				e.preventDefault();
				this.model.destroy({ wait: true })
					.done(function () {
						toastr.success(constants("votingDeletedMessage"));
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
					});
			},
		});

		return votingView;
	});