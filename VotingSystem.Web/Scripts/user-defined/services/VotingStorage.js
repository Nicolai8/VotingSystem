define(["jquery", "angular", "Urls"],
	function ($, angular, Urls, toastr, constants) {
		angular.module("votingSystem.services.votingStorage", [])
			.factory("VotingStorage", function ($resource) {
				var resource = $resource(Urls.Votings + "/:id", { id: "@VotingId" },
					{
						query: {
							method: "GET",
							url: Urls.Votings + "/:pageType/:page",
							isArray: true
						},
						get: { method: "GET" },
						update: { method: "PUT" },
						remove: { method: "DELETE" },
						_save: { method: "POST" },
						total: {
							method: "GET",
							url: Urls.Votings + "/:totalKind",
							transformResponse: function (data) {
								return { total: data };
							}
						}
					}
				);

				resource.save = function (params, data, success, error) {
					if (isValid(data)) {
						resource._save(params, data, success, error);
					}
				};

				return resource;
			});

		function isValid(attrs) {
			if (angular.isUndefined(attrs.VotingName) || attrs.VotingName.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Voting Name"));
				return false;
			}
			if (angular.isUndefined(attrs.StartDate) || attrs.StartDate.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Start Date"));
				return false;
			}
			if (angular.isUndefined(attrs.FinishTime) || attrs.FinishTime.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Finish Date"));
				return false;
			}
			var startDate = new Date(attrs.StartDate);
			var endDate = new Date(attrs.FinishTime);
			if (startDate > endDate) {
				toastr.info(constants("votingCreateValidateDateMessage"));
				return false;
			}
			if (angular.isUndefined(attrs.Questions) || attrs.Questions.length == 0) {
				toastr.info(constants("votingCreateValidateQuestionsCountMessage"));
				return false;
			}

			var validationResult = true;
			$.each(attrs.Questions, function (i, question) {
				if (angular.isUndefined(question.Text) || question.Text.length < 1) {
					toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Question Text"));
					return validationResult = false;
				}
				if (question.Type == 1 && (angular.isUndefined(question.FixedAnswers) || question.FixedAnswers.length < 2)) {
					toastr.info(constants("votingCreateValidateTypeMessage"));
					return validationResult = false;
				}
			});

			return validationResult;
		}
	});

