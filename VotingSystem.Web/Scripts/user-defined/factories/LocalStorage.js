angular.module("votingSystem.factories.localStorage", [])
	.factory("LocalStorage", [
		function () {
			return {
				isVotingAnswered: function (votingId) {
					return localStorage[votingId] == "true";
				},
				setVotingAnswered: function (votingId) {
					localStorage[votingId] = "true";
				},
				setVotingAnswers: function (votingId, answers) {
					localStorage[votingId + ".Answers"] = JSON.stringify(answers);
				},
				getVotingAnswers: function (votingId) {
					return JSON.parse(localStorage[votingId + ".Answers"]);
				}
			}
		}]);