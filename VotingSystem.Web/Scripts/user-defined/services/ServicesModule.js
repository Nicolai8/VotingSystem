﻿define(["angular",
	"services/commentstorage",
	"services/reload",
	"services/userstorage",
	"services/voicestorage",
	"services/votingstorage",
	"services/commentshub"
],
	function (angular) {
		angular.module("votingSystem.services", [
			"votingSystem.services.commentStorage",
			"votingSystem.services.reload",
			"votingSystem.services.userStorage",
			"votingSystem.services.voiceStorage",
			"votingSystem.services.votingStorage",
			"votingSystem.services.commentsHub"
		]);
	});
