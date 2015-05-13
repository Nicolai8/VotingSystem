angular.module("votingSystem.unitOfWork", [
	"votingSystem.factories.commentStorage",
	"votingSystem.factories.userStorage",
	"votingSystem.factories.authStorage",
	"votingSystem.factories.voiceStorage",
	"votingSystem.factories.votingStorage",
	"votingSystem.factories.localStorage"
]).factory("UnitOfWork", ["$injector", function ($injector) {
	return {
		authStorage: function () {
			return $injector.get("AuthStorage");
		},
		commentStorage: function () {
			return $injector.get("CommentStorage");
		},
		localStorage: function () {
			return $injector.get("LocalStorage");
		},
		userStorage: function () {
			return $injector.get("UserStorage");
		},
		voiceStorage: function () {
			return $injector.get("VoiceStorage");
		},
		votingStorage: function () {
			return $injector.get("VotingStorage");
		}
	}
}])