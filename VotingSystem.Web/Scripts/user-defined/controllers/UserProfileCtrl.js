angular.module("votingSystem.controllers.userProfile", [])
	.controller("UserProfileCtrl", ["$scope", "$http", "$route", "$routeParams", "$location", "urls", "constants", "User", "commentsHub",
		function ($scope, $http, $route, $routeParams, $location, urls, constants, User, commentsHub) {
			$scope.userId = $routeParams.userId;
			$scope.isMyPage = angular.isUndefined($scope.userId);
			$scope.pageName = "profile";
			$scope.$route = $route;
			$scope.$location = $location;
			$scope.$routeParams = $routeParams;
			$scope.user = User;

			commentsHub.changePageOnHub();

			$scope.toggleLockUser = function () {
				var oldValue = angular.copy($scope.user.IsBlocked);
				$scope.user.IsBlocked = !$scope.user.IsBlocked;
				$scope.user.$update()
				.then(function () {
					toastr.success(constants["userLockChangedMessage"]);
				}, function () {
					$scope.user.IsBlocked = oldValue;
					toastr.error(constants["userLockChangeFailedMessage"]);
				});
			};

			$scope.removeUser = function () {
				if (confirm("Are you sure that you whan delete " + $scope.user.UserName + "?")) {
					$scope.user.$remove()
					.then(function () {
						toastr.success(constants["userDeletedMessage"]);
						window.history.back();
					}, function () {
						toastr.error(constants["errorOccurredDuringDeletingProcessMessage"]);
					});
				}
			};

			$scope.changePassword = function () {
				var $modal = $("#changePasswordModal");
				if ($modal.find("form[data-validate-form]").data("bootstrapValidator").isValid()) {
					$.post(urls.ProfilePage.ChangePassword, { oldPassword: $scope.oldPassword, newPassword: $scope.newPassword })
					.done(function () {
						toastr.success(constants["passwordChangedMessage"]);
						$modal.modal("hide");
					}).fail(function () {
						toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
					});
				}
			};
			$scope.suggestUser = function () {
				$scope.user.$suggestUser().then(function () {
					toastr.success(constants["userSuggestedToBlockMessage"]);
				}, function () {
					toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
				});
			};

			$scope.changePrivacy = function (privacy) {
				$.post(urls.ProfilePage.ChangePrivacy, { privacy: privacy })
					.done(function () {
						toastr.success(constants["userPrivacySettingUpdatedMessage"]);
						$("#changePrivacyModal").modal("hide");
					}).fail(function () {
						toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
					});
			};
		}]);
