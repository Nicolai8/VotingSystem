define(["model", "Urls", "toastr", "constants"],
function (Model, Urls, toastr, constants) {
	return Model.extend({
		idAttribute: "VotingId",

		defaults: {
			VotingName: "",
		},

		validate: function (attrs, options) {
			if (options.skipValidation) {
				return;
			}
			if (attrs.VotingName.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Voting Name"));
				return constants("votingCreateValidateMessage").replace("{0}", "Voting Name");
			}
			if (attrs.StartDate.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Start Date"));
				return constants("votingCreateValidateMessage").replace("{0}", "Start Date");
			}
			if (attrs.FinishTime.length < 1) {
				toastr.info(constants("votingCreateValidateMessage").replace("{0}", "Finish Date"));
				return constants("votingCreateValidateMessage").replace("{0}", "Finish Date");
			}
			var startDate = new Date(attrs.StartDate);
			var endDate = new Date(attrs.FinishTime);
			if (startDate > endDate) {
				toastr.info(constants("votingCreateValidateDateMessage"));
				return constants("votingCreateValidateDateMessage");
			}
			if (attrs.Questions.length == 0) {
				toastr.info(constants("votingCreateValidateQuestionsCountMessage"));
				return constants("votingCreateValidateQuestionsCountMessage");
			}
			var questionsValidationResult;
			attrs.Questions.every(function (question) {
				questionsValidationResult = question.validate(question.toJSON(), options);
				if (!_.isUndefined(questionsValidationResult)) {
					return false;
				}
			});
			return questionsValidationResult;
		},

		methodUrl: {
			"create": function () {
				return Urls.Votings;
			},
			"delete": function (model) {
				return Urls.Votings + "/" + model.get("VotingId");
			},
			"update": function (model) {
				return Urls.Votings + "/" + model.get("VotingId");
			},
		},

		url: function () {
			return Urls.Votings + "/" + window.state.get("votingId");
		}
	});
});