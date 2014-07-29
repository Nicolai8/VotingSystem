define(["angular",
	"controllers/angular.controller.adminvotings",
	"controllers/angular.controller.comments",
	"controllers/angular.controller.layout",
	"controllers/angular.controller.main",
	"controllers/angular.controller.users",
	"controllers/angular.controller.uservotings",
	"controllers/angular.controller.userprofile",
	"controllers/angular.controller.voices",
	"controllers/angular.controller.voting"
],
	function (angular) {
		angular.module("votingSystem.controllers", [
			"votingSystem.controllers.adminVotings",
			"votingSystem.controllers.comments",
			"votingSystem.controllers.layout",
			"votingSystem.controllers.main",
			"votingSystem.controllers.userProfile",
			"votingSystem.controllers.users",
			"votingSystem.controllers.userVotings",
			"votingSystem.controllers.voices",
			"votingSystem.controllers.voting"
		]);

	});
