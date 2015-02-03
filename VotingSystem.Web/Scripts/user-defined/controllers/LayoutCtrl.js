angular.module("votingSystem.controllers.layout", [])
	.controller("LayoutCtrl", [
		"$scope", "$route", "$routeParams", "$location", "urls", "UnitOfWork", "commentsHub", "notifications",
		function ($scope, $route, $routeParams, $location, urls, UnitOfWork, commentsHub, notifications) {
			$scope.accountName = window.loggedUserName;
			$scope.roles = [];
			$scope.authenticated = window.authenticated;
			$scope.commentsHub = commentsHub;

			$scope.logIn = function (userName, password) {

				var $form = $("#loginForm form[data-validate-form]").data("bootstrapValidator");

				if ($form.isValid()) {
					UnitOfWork.authStorage().login({
						userName: userName,
						password: password,
						rememberMe: false
					}).$promise
						.then(function (data) {
							if (data && data.result) {
								$scope.authenticated = true;
								$scope.accountName = userName;
								$("#loginForm").modal("hide");
								$location.path("/main/1");
							} else {
								notifications.loginFailed();
							}
						}, function () {
							notifications.loginFailed();
						}).finally(function () {
							password = "";
						});
				} else {
					$form.validate();
				}
			};

			$scope.$watch("authenticated", function (newValue) {
				if (newValue) {
					UnitOfWork.authStorage().isInRole({},
						function (data) {
							$scope.roles = data.roles.toLowerCase().split(",");
						});
				}
			});

			$scope.checkUserName = function (userName) {
				UnitOfWork.authStorage().checkUserName(
					{
						userName: userName
					},
					function (data) {
						if (!data.result) {
							notifications.canUseLogin();
						} else {
							notifications.loginAlreadyExists();
						}
					}, function () {
						notifications.loginAlreadyExists();
					});
			};

			$scope.register = function (userName, email) {
				var $form = $("#registerInnerForm").data("bootstrapValidator");
				if ($form.isValid()) {
					UnitOfWork.authStorage().register(
						{
							newUserName: userName,
							email: email
						}, function () {
							$("#registerForm").modal("hide");
							notifications.registrationSucceed();
						}, function () {
							notifications.registrationFailed();
						});
				} else {
					$form.validate();
				}
			};

			$scope.signOut = function ($event) {
				$event.preventDefault();
				UnitOfWork.authStorage().signOut({},
					function () {
						$scope.authenticated = false;
						$scope.accountName = "Account";
						$location.path("/main/1");
					}, function () {
						notifications.logOutFailed();
					});
			};
		}
	]);