define(["angular",
	"services/angular.service.commentstorage",
	"services/angular.service.reload",
	"services/angular.service.userstorage",
	"services/angular.service.voicestorage",
	"services/angular.service.votingstorage"
],
	function (angular) {
		angular.module("votingSystem.services", [
			"votingSystem.services.commentStorage",
			"votingSystem.services.reload",
			"votingSystem.services.userStorage",
			"votingSystem.services.voiceStorage",
			"votingSystem.services.votingStorage"
		]);
	});
