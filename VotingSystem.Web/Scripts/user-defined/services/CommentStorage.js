angular.module("votingSystem.services.commentStorage", [])
	.factory("CommentStorage", ["$resource", "urls", function ($resource, urls) {
		return $resource(urls.Comments + "/:id", { id: "@CommentId" },
			{
				query: {
					method: "GET",
					url: urls.Comments + "/:page",
					isArray: true
				},
				save: { method: "POST" },
				remove: { method: "DELETE", },
				total: {
					method: "GET",
					url: urls.Comments + "/total",
					transformResponse: function (data) {
						return { total: data };
					}
				},
			}
);
	}]);