﻿angular.module("votingSystem.factories.userStorage", [])
	.factory("UserStorage", ["$resource", "urls",
		function ($resource, urls) {
			return $resource(urls.Users + "/:id", { id: "@UserId" },
				{
					query: {
						method: "GET",
						url: urls.Users + "/:pageType/:page",
						isArray: true
					},
					get: {
						method: "GET",
						url: urls.Users + "/profile/:id"
					},
					update: { method: "PUT" },
					remove: { method: "DELETE" },
					total: {
						method: "GET",
						url: urls.Users + "/total/:pageType",
						transformResponse: function (data) {
							return { total: data };
						}
					},
					unsuggestUser: {
						method: "POST",
						url: urls.Users + "/:id/unsuggest"
					},
					suggestUser: {
						method: "POST",
						url: urls.Users + "/:id/suggest"
					},
					getAllRoles: {
						method: "GET",
						url: urls.UsersPage.GetAllRoles,
						isArray: true
					},
					changePassword: {
						method: "POST",
						url: urls.ProfilePage.ChangePassword
					},
					changePrivacy: {
						method: "POST",
						url: urls.ProfilePage.ChangePrivacy
					},
				}
			);
		}]);
