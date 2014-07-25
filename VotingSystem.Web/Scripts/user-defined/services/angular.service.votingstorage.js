define(["angular", "Urls", "angular.resource"],
	function (angular, Urls) {
		return function () {
			angular.module("votingSystem")
				.factory("votingStorage", function ($resource) {
					return $resource(Urls.Votings + "/:id", { id: "@VotingId" },
						{
							query: {
								method: "GET",
								url: Urls.Votings + "/:pageType/:page",
								isArray: true
							},
							get: { method: "GET" },
							update: { method: "PUT" },
							remove: { method: "DELETE" },
							save: { method: "POST" },
							total: {
								method: "GET",
								url: Urls.Votings + "/:totalKind",
								transformResponse: function (data) {
									return { total: data };
								}
							}
						}
					);
				});
		}
	});

