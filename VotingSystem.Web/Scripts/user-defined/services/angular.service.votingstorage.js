define(["Urls", "angular.resource"],
	function (Urls) {
		return function (module) {
			module.factory("votingStorage",
				function ($resource) {
					return $resource(Urls.Votings + "/:id", { id: "@VotingId" },
						{
							query: {
								method: "GET",
								url: Urls.Votings + "/:pageType/:page",
								isArray: true
							},
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
		};
	});

