define(["jquery", "underscore", "backbone", "router", "Urls", "stateModel", "mainPage", "commentsPage",
		"profilePage", "usersPage", "voicesPage", "votingPage", "userVotingsPage",
		"adminVotingsPage", "toastr", "constants", "spin", "bootstrap", "stickit", "bootstrapValidator"],
	function ($, _, Backbone, Router, Urls, StateModel, MainPage, CommentsPage, UserProfilePage,
		UsersPage, VoicesPage, VotingPage, UserVotingsPage, AdminVotingsPage, toastr, constants, Spinner) {

		var bacboneSync = Backbone.sync;
		Backbone.sync = function (method, model, options) {
			$("#preloader").show();
			return bacboneSync.apply(this, [method, model, options])
				.always(function () {
					$("#preloader").hide();
				});
		};

		var AppView = Backbone.View.extend({
			el: $("body"),

			events: {
				"hidden.bs.modal [role='dialog']": "clearModalFieldsAfterClose",
				"click #mainSearchButton": "search",
				"click #logInButton": "logIn",
				"keypress #password": "logInOnEnter",
				"focusout #registerUserName": "checkUserName",
				"click #registerButton": "register",
				"click #signOut": "signOut",
			},

			bindings: {
				".for-loggedIn": {
					observe: "authenticated",
					visible: function (value) {
						return value;
					}
				},
				".for-notLoggedIn": {
					observe: "authenticated",
					visible: function (value) {
						return !value;
					}
				},
				".for-moderator": {
					observe: "roles",
					visible: function (value) {
						return value.indexOf("admin") != -1 || value.indexOf("moderator") != -1;
					},
				},
				".for-administrator": {
					observe: "roles",
					visible: function (value) {
						return value.indexOf("admin") != -1;
					},
				},
				"[role='loggedUserName']": "loggedUserName",
			},

			initialize: function () {
				window.state = new StateModel();
				this.model = window.state;

				this.loadSpinner();

				this.defineCustomBinds();

				window.state.on("change:pageName=page403", this.errorPage);
				window.state.on("change:pageName=page404", this.errorPage);
				window.state.on("change:pageName=page500", this.errorPage);
				window.state.on("change:authenticated", this.getRole);
				window.state.set("authenticated", window.authenticated);
				window.state.set("loggedUserName", window.loggedUserName);

				this.initializePages();
				this.stickit();
			},
			defineCustomBinds: function () {
				window.pages = ["votingpage", "myvoicespage", "myvotingspage", "userspage",
					"suggesteduserspage", "profilepage", "mainpage", "mycommentspage",
					"adminvotingspage", "page403", "page404", "page500"];
				$.each(window.pages, function (i, page) {
					window.state.defineCustomBind("pageName", page);
				});
			},
			initializePages: function () {
				this.votingPage = new VotingPage();
				this.mainPage = new MainPage();
				this.userVotingsPage = new UserVotingsPage();
				this.voicesPage = new VoicesPage();
				this.usersPage = new UsersPage({ type: "userspage" });
				this.suggestedUsersPage = new UsersPage({ type: "suggesteduserspage" });
				this.profilePage = new UserProfilePage();
				this.adminVotingsPage = new AdminVotingsPage();
				this.commentsPage = new CommentsPage();
			},

			clearModalFieldsAfterClose: function (e) {
				var $modal = $(e.currentTarget);
				$modal.find(":text,textarea,:password").each(function () {
					$(this).val("").parent().removeClass("has-error").removeClass("has-success");
				});
				$modal.find(":checked").each(function () {
					$(this).attr("checked", "false");
				});
				$modal.find(".wmd-preview").empty();
				$modal.find("form[data-validate-form='true'], form[data-validate-form='false']").each(function () {
					var validateForm = $(this).data("bootstrapValidator");
					if (!_.isUndefined(validateForm)) {
						validateForm.resetForm();
					}
				});
			},

			loadSpinner: function () {
				var opts = {
					lines: 10,
					length: 15,
					width: 8,
					radius: 20,
					trail: 60,
				};
				var spinner = new Spinner(opts).spin($("#preloader")[0]);
			},

			search: function () {
				var searchTextbox = $(this.el).find("#mainSearch");
				var searchText = searchTextbox.val();
				if (searchText.length > 0) {
					searchTextbox.val("");
					window.router.navigate("mainpage/1/" + searchText, { trigger: true, replace: true });
				}
			},

			errorPage: function () {
				$("#breadcrumb").empty();
				$("#" + window.state.get("pageName")).addClass("open-page").show();
			},

			logIn: function () {
				var that = this;
				var userName = $("#userName").val();
				var password = $("#password").val();

				if ($("#loginForm form[data-validate-form='true']").data("bootstrapValidator").isValid()) {
					$.ajax({
						url: Urls.LoginPage.Login,
						type: "POST",
						data: { userName: userName, password: password, rememberMe: false },
						beforeSend: function () {
							that.votingPage.stopCommentsHub();
						}
					}).done(function (data) {
						if (data && data.result) {
							window.state.set("authenticated", true);
							window.state.set("loggedUserName", userName);
							$("#loginForm").modal("hide");
							window.router.navigate("mainpage/1", { trigger: true });
						} else {
							toastr.error(constants("loginFailedMessage"));
						}
					}).fail(function () {
						toastr.error(constants("loginFailedMessage"));
					}).always(function () {
						that.votingPage.startCommentsHub();
						$("#password").val("");
					});
				}
			},

			logInOnEnter: function (e) {
				if (e.which == 13) {
					$("#logInButton").click();
				}
			},

			checkUserName: function () {
				if ($("#registerInnerForm").data("bootstrapValidator").isValid()) {
					$.get(Urls.LoginPage.CheckUserName, { userName: $("#registerUserName").val() })
						.done(function () {
							toastr.success(constants("canUseLoginMessage"));
						}).fail(function () {
							toastr.warning(constants("loginAlreadyExistsMessage"));
						});
				}
			},

			register: function (e) {
				e.preventDefault();
				if ($("#registerInnerForm").data("bootstrapValidator").isValid()) {
					var userName = $("#registerUserName").val();
					var email = $("#registerEmail").val();
					$.post(Urls.LoginPage.Register, { newUserName: userName, email: email })
						.done(function () {
							$("#registerForm").modal("hide");
							toastr.success(constants("registrationSucceedMessage"));
						}).fail(function () {
							toastr.error(constants("registrationFailedMessage"));
						});
				}
			},
			signOut: function (e) {
				var that = this;
				e.preventDefault();
				$.ajax({
					url: Urls.LoginPage.LogOff,
					type: "POST",
					beforeSend: function () {
						that.votingPage.stopCommentsHub();
					}
				}).done(function () {
					that.model.set("authenticated", false);
					window.state.set("loggedUserName", "Account");
					window.router.navigate("mainpage/1", { trigger: true });
				}).fail(function () {
					toastr.error(constants("logOutFailedMessage"));
				}).complete(function () {
					that.votingPage.startCommentsHub();
				});
			},
			getRole: function () {
				if (window.state.get("authenticated")) {
					$.get(Urls.IsInRole).done(function (data) {
						window.state.set("roles", data.toLowerCase());
					});
				}
			}
		});

		$(function () {
			$('form[data-validate-form="true"]').bootstrapValidator({
				message: 'This field is required',
				feedbackIcons: {
					valid: 'glyphicon glyphicon-ok',
					invalid: 'glyphicon glyphicon-remove',
					validating: 'glyphicon glyphicon-refresh'
				},
				excluded: [':disabled'],
			});
			window.urls = Urls;
			var appView = new AppView();
			window.router = new Router();
			Backbone.history.start();

		});
	}
);