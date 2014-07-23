define(["Urls", "angular.resource"],
	function (Urls) {
		return function (module) {
			module.factory("voiceStorage",
				function ($resource) {
					return $resource(Urls.Voices, {},
						{
							query: {
								method: "GET",
								url: Urls.Voices + "?page=:page&size=10",
								isArray: true
							},
							save: { method: "POST" },
							total: {
								method: "GET",
								url: Urls.Voices + "/total",
								transformResponse: function (data) {
									return { total: data };
								}
							},
						}
					);
				});
		};
	});

