define(["jquery", "underscore", "userView", "profileModel", "Urls", "toastr", "constants", "templates", "jquery.fileUpload", "stickit"],
	function ($, _, UserView, ProfileModel, Urls, toastr, constants, templates) {
		var userProfilePageView = UserView.extend({
			el: $("#profilePage"),

			events: {
				"click .toggle-lock-user-button": "toggleLockUser",
				"click .remove-user-button": "removeUser",
				"click .suggest-user-to-block-button": "suggestUser",
				"click #changePassword": "changePassword",
				"click #myProfileChangePrivacy": "changePrivacy"
			},

			bindings: {
				"#profileModal .userProfile": {
					observe: "UserName",
					visible: function () {
						return window.state.get("userName").length != 0;
					}
				},
				"#profileModal .myProfile": {
					observe: "UserName",
					visible: function () {
						return window.state.get("userName").length == 0;
					}
				},
				"#profileModal .toggle-lock-user-button": {
					observe: "IsBlocked",
					updateMethod: "html",
					onGet: function (value) {
						return templates.get("ToggleProfileLockButton")({ IsBlocked: value });
					}
				},
				"#profileModalUserName": "UserName",
				"#profileModalEmail": "Email",
				"#profileModalRegisterDate": "CreateDate",
				"#userPicture": {
					attributes: [{
						observe: "PictureUrl",
						name: "src",
					}]
				}
			},

			initialize: function () {
				this.model = new ProfileModel();
				this.model.on("destroy", this.unrender, this);
				this.changePicture();
				this.icheck();
				window.state.on("change:pageName=profilepage", this.render, this);
				this.model.on("change:Privacy", this.setPrivacy, this);
				this.$el.on("hidden.bs.modal", "#profileModal", function () { window.router.back(); });
				this.stickit();
			},
			render: function () {
				var that = this;
				this.model.set("UserName", "");
				this.model.fetch({
					success: function () {
						that.fillData();
					},
					error: function (context, code) {
						that.$el.find("#profileModal").modal("hide");
						window.router.navigate(code.status == 403 ? "page403" : "page500", { trigger: true });
					}
				});
			},
			unrender: function () {
				$("#profileModal").modal("hide");
			},

			fillData: function () {
				this.$el.find("#profileModal").modal("show");
				this.setPrivacy();
			},
			icheck:function() {
				this.$el.find(":radio").iCheck({
					radioClass: "iradio_flat-red"
				});
			},
			setPrivacy: function () {
				this.$el.find("#changePrivacyModal form :radio[value=" + this.model.get("Privacy") + "]").iCheck("check");
			},
			changePassword: function () {
				var $modal = this.$el.find("#changePasswordModal");
				var oldPassword = $modal.find("#oldPassword").val();
				var newPassword = $modal.find("#newPassword").val();
				if ($modal.find("form[data-validate-form='true']").data("bootstrapValidator").isValid()) {
					$.post(Urls.ProfilePage.ChangePassword, { oldPassword: oldPassword, newPassword: newPassword })
						.done(function () {
							toastr.success(constants("passwordChangedMessage"));
							$modal.modal("hide");
						}).fail(function () {
							toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
						});
				}
			},
			suggestUser: function () {
				$.post((Urls.Users + "/" + this.model.get("UserName") + "/suggest"))
					.done(function () {
						toastr.success(constants("userSuggestedToBlockMessage"));
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					});
			},
			changePicture: function () {
				var that = this;
				$("#pictureUpload").fileupload({
					done: function (e, data) {
						that.$el.find("#progress").hide();
						toastr.success(constants("userImageUpdatedMessage"));
						that.model.set("PictureUrl", data.result);
					},
					progressall: function (e, data) {
						that.$el.find("#progress").show();
						var progress = parseInt(data.loaded / data.total * 100, 10);
						that.$el.find("#progress .progress-bar").css(
							'width',
							progress + '%'
						);
					},
					fail: function () {
						that.$el.find("#progress").hide();
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					}
				});
			},
			changePrivacy: function () {
				var that = this;
				var privacyValue = this.$el.find("#changePrivacyModal form :radio:checked").val();
				$.post(Urls.ProfilePage.ChangePrivacy, { privacy: privacyValue })
					.done(function () {
						toastr.success(constants("userPrivacySettingUpdatedMessage"));
						that.model.set("Privacy", privacyValue);
						that.$el.find("#changePrivacyModal").modal("hide");
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					});
			}
		});

		return userProfilePageView;
	})