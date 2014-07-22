define(["Urls", "angular.resource"],
	function (Urls) {
		return function (module) {
			module.factory("userStorage",
				function ($resource) {
					return $resource(Urls.Users + "/:id", { id: "@UserName" },
						{
							query: {
								method: "GET",
								url: Urls.Users + "/:pageType/:page",
								isArray: true
							},
							update: { method: "PUT", },
							remove: { method: "DELETE", },
							total: {
								method: "GET",
								url: Urls.Users + "/total/:pageType",
								transformResponse: function (data) {
									return { total: data };
								}
							},
							unsuggestUser: {
								method: "POST",
								url: Urls.Users + "/:id/unsuggest",
							},
							suggestUser: {
								method: "POST",
								url: Urls.Users + "/:id/suggest",
							}
						}
					);
				});
		};
	});

