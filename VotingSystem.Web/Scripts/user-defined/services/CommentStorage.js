﻿define(["angular", "Urls"],
	function (angular, Urls) {
		angular.module("votingSystem.services.commentStorage", [])
			.factory("CommentStorage", function ($resource) {
				return $resource(Urls.Comments + "/:id", { id: "@CommentId" },
					{
						query: {
							method: "GET",
							url: Urls.Comments + "/:page",
							isArray: true
						},
						save: { method: "POST" },
						remove: { method: "DELETE", },
						total: {
							method: "GET",
							url: Urls.Comments + "/total",
							transformResponse: function (data) {
								return { total: data };
							}
						},
					}
				);
			});
	});
