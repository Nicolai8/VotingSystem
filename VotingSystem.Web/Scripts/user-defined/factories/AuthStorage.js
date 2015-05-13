angular.module("votingSystem.factories.authStorage", [])
	.factory("AuthStorage", ["$resource", "$http", "urls", "commentsHub",
		function ($resource, $http, urls, commentsHub) {
			return $resource("", {},
			{
				login: {
					method: "POST",
					url: urls.LoginPage.Login,
					transformRequest: $http.defaults.transformRequest.concat([
						function (request) {
							commentsHub.stopCommentsHub();
							return request;
						}
					])
				},
				register: {
					method: "POST",
					url: urls.LoginPage.Register
				},
				checkUserName: {
					method: "GET",
					url: urls.LoginPage.CheckUserName
				},
				isInRole: {
					method: "GET",
					url: urls.IsInRole,
					transformResponse: function (data) {
						return { roles: data };
					}
				},
				signOut: {
					method: "POST",
					url: urls.LoginPage.LogOff,
					transformRequest: $http.defaults.transformRequest.concat([
						function (request) {
							commentsHub.stopCommentsHub();
							return request;
						}
					])
				}
			}
			);
		}]);
