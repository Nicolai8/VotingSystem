angular.module("votingSystem.services.votingStorage", [])
	.factory("VotingStorage", ["$resource", "urls",
		function ($resource, urls) {
			return $resource(urls.Votings + "/:id", { id: "@VotingId" },
				{
					query: {
						method: "GET",
						url: urls.Votings + "/:pageType/:page",
						isArray: true
					},
					get: { method: "GET" },
					update: { method: "PUT" },
					remove: { method: "DELETE" },
					save: { method: "POST" },
					total: {
						method: "GET",
						url: urls.Votings + "/:totalKind",
						transformResponse: function (data) {
							return { total: data };
						}
					}
				}
			);
		}]);

