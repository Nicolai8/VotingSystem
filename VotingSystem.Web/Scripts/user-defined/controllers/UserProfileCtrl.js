angular.module("votingSystem.controllers.userProfile", [])
	.controller("UserProfileCtrl", ["$scope", "$routeParams", "urls", "User", "commentsHub", "notifications",
		function ($scope, $routeParams, urls, User, commentsHub, notifications) {
			$scope.userId = $routeParams.userId;
			$scope.isMyPage = angular.isUndefined($scope.userId);
			$scope.pageName = "profile";
			$scope.user = User;

			commentsHub.changePageOnHub();

			$scope.toggleLockUser = function () {
				var oldValue = angular.copy($scope.user.IsBlocked);
				$scope.user.IsBlocked = !$scope.user.IsBlocked;
				$scope.user.$update()
					.then(function () {
						notifications.userLockChanged();
					}, function () {
						$scope.user.IsBlocked = oldValue;
						notifications.userLockChangeFailed();
					});
			};

			$scope.removeUser = function () {
				if (confirm("Are you sure that you whan delete " + $scope.user.UserName + "?")) {
					$scope.user.$remove()
						.then(function () {
							notifications.userDeleted();
							window.history.back();
						}, function () {
							notifications.deletingError();
						});
				}
			};

			$scope.changePassword = function () {
				var $modal = $("#changePasswordModal");
				if ($modal.find("form[data-validate-form]").data("bootstrapValidator").isValid()) {
					User.$changePassword(
						{
							oldPassword: $scope.oldPassword,
							newPassword: $scope.newPassword
						}, function () {
							notifications.passwordChanged();
							$modal.modal("hide");
						}, function () {
							notifications.savingError();
						});
				}
			};
			$scope.suggestUser = function () {
				$scope.user.$suggestUser()
					.then(function () {
						notifications.userSuggestedToBlock();
					}, function () {
						notifications.savingError();
					});
			};

			$scope.changePrivacy = function (privacy) {
				User.$changePrivacy(
					{
						privacy: privacy
					}, function () {
						notifications.userPrivacyUpdated();
						$("#changePrivacyModal").modal("hide");
					}, function () {
						notifications.savingError();
					});
			};
		}]);
