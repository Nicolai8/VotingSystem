define(["jquery", "angular", "Urls", "constants", "toastr", "bootstrap", "bootstrapValidator", "angular.route", "jquery.fileUpload"],
	function ($, angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller("UserProfileController", function ($scope, user, userStorage, $http, $route, $routeParams, $location) {
					$scope.userName = $routeParams.userName;
					$scope.isMyPage = angular.isUndefined($scope.userName);
					$scope.pageName = "profilepage";
					$scope.$route = $route;
					$scope.$location = $location;
					$scope.$routeParams = $routeParams;
					$scope.user = user;

					$scope.toggleLockUser = function () {
						var oldValue = angular.copy($scope.user.IsBlocked);
						$scope.user.IsBlocked = !$scope.user.IsBlocked;
						$scope.user.$update()
							.then(function () {
								toastr.success(constants("userLockChangedMessage"));
							}, function () {
								$scope.user.IsBlocked = oldValue;
								toastr.error(constants("userLockChangeFailedMessage"));
							});
					};

					$scope.removeUser = function () {
						if (confirm("Are you sure that you whan delete " + $scope.user.UserName + "?")) {
							$scope.user.$remove()
								.then(function () {
									toastr.success(constants("userDeletedMessage"));
									window.history.back();
								}, function () {
									toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
								});
						}
					};

					$scope.changePassword = function () {
						var $modal = $("#changePasswordModal");
						var oldPassword = $modal.find("#oldPassword").val();
						var newPassword = $modal.find("#newPassword").val();
						if ($modal.find("form[data-validate-form]").data("bootstrapValidator").isValid()) {
							$.post(Urls.ProfilePage.ChangePassword, { oldPassword: oldPassword, newPassword: newPassword })
								.done(function () {
									toastr.success(constants("passwordChangedMessage"));
									$modal.modal("hide");
								}).fail(function () {
									toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
								});
						}
					};
					$scope.suggestUser = function () {
						$scope.user.$suggestUser().then(function () {
							toastr.success(constants("userSuggestedToBlockMessage"));
						}, function () {
							toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
						});
					};

					var $progress = $("#progress");
					$("#pictureUpload").fileupload({
						url: Urls.ProfilePage.ChangePicture,
						done: function (e, data) {
							$progress.hide();
							toastr.success(constants("userImageUpdatedMessage"));
							$scope.user.PictureUrl = data.result;
						},
						progressall: function (e, data) {
							$progress.show();
							var progress = parseInt(data.loaded / data.total * 100, 10);
							$progress.find(".progress-bar").css(
								"width",
								progress + "%"
							);
						},
						fail: function () {
							$progress.hide();
							toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
						}
					});

					$scope.changePrivacy = function () {
						var privacyValue = $("#changePrivacyModal form :radio:checked").val();
						$.post(Urls.ProfilePage.ChangePrivacy, { privacy: privacyValue })
							.done(function () {
								toastr.success(constants("userPrivacySettingUpdatedMessage"));
								$("#changePrivacyModal").modal("hide");
							}).fail(function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};
				});
		};
	});

