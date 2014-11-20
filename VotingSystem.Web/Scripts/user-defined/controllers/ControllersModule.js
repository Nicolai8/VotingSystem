define(["angular",
	"controllers/AdminVotingsCtrl",
	"controllers/CommentsCtrl",
	"controllers/LayoutCtrl",
	"controllers/MainCtrl",
	"controllers/UsersCtrl",
	"controllers/UserVotingsCtrl",
	"controllers/UserProfileCtrl",
	"controllers/VoicesCtrl",
	"controllers/VotingCtrl"
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
