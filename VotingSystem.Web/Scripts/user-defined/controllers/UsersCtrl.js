angular.module("votingSystem.controllers.users", [])
	.controller("UsersCtrl", ["$scope", "$routeParams", "$location", "urls", "constants",
		"UnitOfWork", "commentsHub", "reloadDataAfterUserAction", "notifications",
		function ($scope, $routeParams, $location, urls, constants, UnitOfWork, commentsHub,
			reloadDataAfterUserAction, notifications) {
			$scope.isSuggested = angular.isDefined($routeParams.suggested) && $routeParams.suggested == "suggested";
			$scope.pageName = "users";
			$scope.pageType = $scope.isSuggested ? "SuggestedUsers" : "Users",
			$scope.breadCrumbItemName = "Admin" + ($scope.isSuggested ? " Suggested" : "") + " Users",
			$scope.total = 1;
			$scope.roles = [];
			$scope.editUserRoles = [];

			commentsHub.changePageOnHub();

			UnitOfWork.userStorage().getAllRoles({}, function (roles) {
				$scope.roles = roles;
			});

			UnitOfWork.userStorage().query({ pageType: $scope.pageType, page: $routeParams.pageNumber },
				function (users) {
					$scope.users = users;
					UnitOfWork.userStorage().total({ pageType: $scope.pageType },
					function (response) {
						$scope.total = response.total;
					});
				});

			$scope.changeUserRoles = function (user) {
				$scope.editUser = user;
				$scope.editUserRoles = angular.copy(user.Roles);
			};

			$scope.changeRoles = function () {
				var oldRoles = angular.copy($scope.editUser.Roles);
				$scope.editUser.Roles = $scope.editUserRoles;
				var $modal = $("#changeRolesModal");
				var $form = $modal.find("form[data-validate-form]").data("bootstrapValidator");
				if ($form.isValid()) {
					$scope.editUser.$update()
						.then(function () {
							$modal.modal("hide");
							notifications.userRolesChanged();
						}, function () {
							$scope.editUser.Roles = oldRoles;
							notifications.savingError();
						});
				} else {
					$form.validate();
				}
			};

			$scope.toggleLockUser = function (user) {
				user.IsBlocked = !user.IsBlocked;
				user.$update()
					.then(function () {
						notifications.userLockChanged();
					}, function () {
						notifications.userLockChangeFailed();
					});
			};

			$scope.removeUser = function (user) {
				if (confirm(constants["userDeleteConfirmMessage"].replace("{userName}", user.UserName))) {
					user.$remove()
						.then(function () {
							$scope.users.splice($scope.users.indexOf(user), 1);
							notifications.userDeleted();
							reloadDataAfterUserAction($scope.users.length, "/" + $scope.pageName + "/{pageNumber}/" + $routeParams.suggested);
						}, function () {
							notifications.deletingError();
						});
				}
			};

			$scope.unsuggestUser = function (user) {
				user.$unsuggestUser()
					.then(function () {
						$scope.users.splice($scope.users.indexOf(user), 1);
						notifications.userUnSuggestToBlock();
						reloadDataAfterUserAction($scope.users.length, "/" + $scope.pageName + "/{pageNumber}/" + $routeParams.suggested);
					}, function () {
						notifications.savingError();
					});
			};

			$scope.toggleRoleSelection = function (roleName) {
				var index = $scope.editUserRoles.indexOf(roleName);
				if (index > -1) {
					$scope.editUserRoles.splice(index, 1);
				}
				else {
					$scope.editUserRoles.push(roleName);
				}
			};
		}]);