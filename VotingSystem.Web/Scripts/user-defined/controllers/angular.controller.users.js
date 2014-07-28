define(["jquery", "angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function ($, angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller("UsersController", function ($scope, userStorage, $reload, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.isSuggested = angular.isDefined($routeParams.suggested) && $routeParams.suggested == "suggested";
					$scope.pageName = "userspage";
					$scope.pageType = $scope.isSuggested ? "SuggestedUsers" : "Users",
					$scope.breadCrumbItemName = "Admin" + ($scope.isSuggested ? " Suggested" : "") + " Users",
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$route = $route;
					$scope.$location = $location;
					$scope.$routeParams = $routeParams;
					$scope.roles = [];
					$scope.editUserRoles = [];
					$scope.reload = $reload;

					$scope.$parent.changePageOnHub();

					$http.get(Urls.UsersPage.GetAllRoles)
						.success(function (roles) {
							$scope.roles = roles;
						});

					userStorage.query({ pageType: $scope.pageType, page: $scope.page },
						function (users) {
							$scope.users = users;
							userStorage.total({ pageType: $scope.pageType },
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
									toastr.success(constants("userRolesChangedMessage"));
								}, function () {
									$scope.editUser.Roles = oldRoles;
									toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
								});
						} else {
							$form.validate();
						}
					};

					$scope.toggleLockUser = function (user) {
						user.IsBlocked = !user.IsBlocked;
						user.$update()
							.then(function () {
								toastr.success(constants("userLockChangedMessage"));
							}, function () {
								toastr.error(constants("userLockChangeFailedMessage"));
							});
					};

					$scope.removeUser = function (user) {
						if (confirm("Are you sure that you whan delete " + user.UserName + "?")) {
							user.$remove()
								.then(function () {
									$scope.users.splice($scope.users.indexOf(user), 1);
									toastr.success(constants("userDeletedMessage"));
									$scope.reload($scope, $scope.users.length, "/" + $scope.pageName + "/{pageNumber}/" + $scope.$routeParams.suggested);
								}, function () {
									toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
								});
						}
					};

					$scope.unsuggestUser = function (user) {
						user.$unsuggestUser()
							.then(function () {
								$scope.users.splice($scope.users.indexOf(user), 1);
								toastr.success(constants("userUnSuggestMessage"));
								$scope.reload($scope, $scope.users.length, "/" + $scope.pageName + "/{pageNumber}/" + $scope.$routeParams.suggested);
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
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
				});
		};
	});

