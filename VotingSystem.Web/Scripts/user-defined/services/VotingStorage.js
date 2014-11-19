define(["jquery", "angular", "Urls", "toastr", "constants"],
	function ($, angular, Urls, toastr, constants) {
	    angular.module("votingSystem.services.votingStorage", [])
			.factory("VotingStorage", function ($resource) {
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
	});

