angular.module("votingSystem.services.userStorage", [])
	.factory("UserStorage", ["$resource", "urls",
		function ($resource, urls) {
			return $resource(urls.Users + "/:id", { id: "@UserName" },
				{
					query: {
						method: "GET",
						url: urls.Users + "/:pageType/:page",
						isArray: true
					},
					get: {
						method: "GET",
						url: urls.ProfilePage.GetProfile
					},
					update: { method: "PUT", },
					remove: { method: "DELETE", },
					total: {
						method: "GET",
						url: urls.Users + "/total/:pageType",
						transformResponse: function (data) {
							return { total: data };
						}
					},
					unsuggestUser: {
						method: "POST",
						url: urls.Users + "/:id/unsuggest",
					},
					suggestUser: {
						method: "POST",
						url: urls.Users + "/:id/suggest",
					}
				}
			);
		}]);
