angular.module("votingSystem.services.voiceStorage", [])
	.factory("VoiceStorage", ["$resource", "urls",
		function ($resource, urls) {
			return $resource(urls.Voices, {},
				{
					query: {
						method: "GET",
						url: urls.Voices + "?page=:page&size=10",
						isArray: true
					},
					getResults: {
						mrthod: "GET",
						url: urls.Voices + "/:votingId",
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
						url: urls.Voices + "/total",
						transformResponse: function (data) {
							return { total: data };
						}
					}
				}
			);
		}]);

