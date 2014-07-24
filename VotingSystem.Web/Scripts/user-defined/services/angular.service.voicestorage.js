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
							getResults: {
								mrthod: "GET",
								url: Urls.Voices + "/:votingId",
								isArray: true
							},
							save: {
								method: "POST",
								transformResponse: function (data) {
									return { total: data };
								}
							},
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

