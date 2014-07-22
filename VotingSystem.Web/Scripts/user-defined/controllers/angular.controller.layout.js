define(["jquery", "angular", "Urls", "toastr", "constants","spin", "angular.route", "bootstrap", "bootstrapValidator"],
	function ($, angular, Urls, toastr, constants, Spinner) {
		return function (controllersModule) {
			controllersModule
				.controller('LayoutController', function ($scope, $route, $routeParams, $location) {
					$scope.accountName = window.loggedUserName;
					$scope.roles = [];
					$scope.authenticated = window.authenticated;
					$scope.$location = $location;

					$scope.logIn = function () {
						var userName = $("#userName").val();
						var password = $("#password").val();

						if ($("#loginForm form[data-validate-form='']").data("bootstrapValidator").isValid()) {
							$.ajax({
								url: Urls.LoginPage.Login,
								type: "POST",
								data: { userName: userName, password: password, rememberMe: false },
								//beforeSend: function () {
								//	that.votingPage.stopCommentsHub();
								//}
							}).done(function (data) {
								if (data && data.result) {
									$scope.authenticated = true;
									$scope.accountName = userName;
									$scope.$apply();
									$("#loginForm").modal("hide");
									$scope.$location.path("/mainpage/1");
								} else {
									toastr.error(constants("loginFailedMessage"));
								}
							}).fail(function () {
								toastr.error(constants("loginFailedMessage"));
							}).always(function () {
								//that.votingPage.startCommentsHub();
								$("#password").val("");
							});
						}
					};

					$scope.$watch("authenticated", function (newValue) {
						if (newValue) {
							$.get(Urls.IsInRole).done(function (data) {
								$scope.roles = data.toLowerCase().split(",");
								$scope.$apply();
							});
						}
					});

					$scope.logInOnEnter = function ($event) {
						if ($event.which == 13) {
							$scope.logIn();
						}
					};

					$scope.checkUserName = function () {
						$.get(Urls.LoginPage.CheckUserName, { userName: $scope.registerUserName })
							.done(function (data) {
								if (!data.result) {
									toastr.success(constants("canUseLoginMessage"));
								} else {
									toastr.warning(constants("loginAlreadyExistsMessage"));
								}
							}).fail(function () {
								toastr.warning(constants("loginAlreadyExistsMessage"));
							});
					};

					$scope.register = function () {
						if ($("#registerInnerForm").data("bootstrapValidator").isValid()) {
							var userName = $scope.registerUserName;
							var email = $scope.registerEmail;
							$.post(Urls.LoginPage.Register, { newUserName: userName, email: email })
								.done(function () {
									$("#registerForm").modal("hide");
									toastr.success(constants("registrationSucceedMessage"));
								}).fail(function () {
									toastr.error(constants("registrationFailedMessage"));
								});
						}
					};

					$scope.signOut = function ($event) {
						$event.preventDefault();
						$.ajax({
							url: Urls.LoginPage.LogOff,
							type: "POST",
							//beforeSend: function () {
							//	that.votingPage.stopCommentsHub();
							//}
						}).done(function () {
							$scope.authenticated = false;
							$scope.accountName = "Account";
							$scope.$location.path("/mainpage/1");
							$scope.$apply();
						}).fail(function () {
							toastr.error(constants("logOutFailedMessage"));
						}).complete(function () {
							//that.votingPage.startCommentsHub();
						});
					};

					$("body").on("hidden.bs.modal", "[role='dialog']", function (e) {
						var $modal = $(e.currentTarget);
						$modal.find(":text,textarea,:password").each(function () {
							$(this).val("");
						});
						$modal.find(":checked").each(function () {
							$(this).attr("checked", "false");
						});
						$modal.find(".wmd-preview").empty();
						$modal.find("form[data-validate-form=''], form[data-validate-form='false']").each(function () {
							var validateForm = $(this).data("bootstrapValidator");
							if (angular.isDefined(validateForm)) {
								validateForm.resetForm();
							}
						});
					});
					
					var opts = {
						lines: 10,
						length: 15,
						width: 8,
						radius: 20,
						trail: 60,
					};
					var spinner = new Spinner(opts).spin($("#preloader")[0]);
				});
		};
	});


