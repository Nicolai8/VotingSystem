define(["Urls", "angular.resource"],
	function (Urls) {
		return function (module) {
			module.factory("votingStorage",
				function ($resource) {
					return $resource(Urls.Votings + "/:id",
						{},
						{
							query: {
								method: "GET",
								url: Urls.Votings + "/:pageType/:page",
								isArray: true
							},
							put: {
								method: "PUT"
							},
							delete: {
								method: "DELETE",
							},
							post: {
								method: "POST"
							},
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

