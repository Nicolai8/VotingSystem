﻿var d = new Date();
requirejs.config({
	"baseUrl": "/VotingSystem.Web/scripts/libraries",
	"paths": {
		"user-defined": "../user-defined",
		"controllers": "../user-defined/controllers",
		"directives": "../user-defined/directives",
		async: "async",
		goog: "goog",
		propertyParser: "propertyParser",
		"signalr": "jquery.signalR-2.1.0.min",
		"signalr.hubs": "/VotingSystem.Web/signalr/hubs?",
		"bootpag": "jquery.bootpag",
		"bootstrapDatePicker": "bootstrap-datetimepicker",
		"markdown": "jquery.pagedown-bootstrap.combined",
		"moment": "moment-2.4.0",
		"constants": "../user-defined/constants",
		"templates": "../user-defined/templates",
		"angular": "angular/angular",
		"angular.route": "angular/angular-route",

		//"router": "../user-defined/backbone.router.js?" + (cacheScripts ? "" : d.getTime()),

		//"model": "../user-defined/models/backbone.model.js?" + (cacheScripts ? "" : d.getTime()),
		//"stateModel": "../user-defined/models/backbone.model.state.js?" + (cacheScripts ? "" : d.getTime()),
		//"commentModel": "../user-defined/models/backbone.model.comment.js?" + (cacheScripts ? "" : d.getTime()),
		//"profileModel": "../user-defined/models/backbone.model.profile.js?" + (cacheScripts ? "" : d.getTime()),
		//"userModel": "../user-defined/models/backbone.model.user.js?" + (cacheScripts ? "" : d.getTime()),
		//"voiceModel": "../user-defined/models/backbone.model.voice.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingModel": "../user-defined/models/backbone.model.voting.js?" + (cacheScripts ? "" : d.getTime()),
		//"fixedAnswerModel": "../user-defined/models/backbone.model.fixedanswer.js?" + (cacheScripts ? "" : d.getTime()),
		//"questionModel": "../user-defined/models/backbone.model.question.js?" + (cacheScripts ? "" : d.getTime()),

		//"commentsCollection": "../user-defined/collections/backbone.collection.comments.js?" + (cacheScripts ? "" : d.getTime()),
		//"usersCollection": "../user-defined/collections/backbone.collection.users.js?" + (cacheScripts ? "" : d.getTime()),
		//"voicesCollection": "../user-defined/collections/backbone.collection.voices.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingsCollection": "../user-defined/collections/backbone.collection.votings.js?" + (cacheScripts ? "" : d.getTime()),
		//"fixedAnswersCollection": "../user-defined/collections/backbone.collection.fixedanswers.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingResultCollection": "../user-defined/collections/backbone.collection.votingresult.js?" + (cacheScripts ? "" : d.getTime()),
		//"questionsCollection": "../user-defined/collections/backbone.collection.questions.js?" + (cacheScripts ? "" : d.getTime()),

		//"view": "../user-defined/views/backbone.view.js?" + (cacheScripts ? "" : d.getTime()),
		//"commentView": "../user-defined/views/backbone.view.comment.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingView": "../user-defined/views/backbone.view.voting.js?" + (cacheScripts ? "" : d.getTime()),
		//"voiceView": "../user-defined/views/backbone.view.voice.js?" + (cacheScripts ? "" : d.getTime()),
		//"userView": "../user-defined/views/backbone.view.user.js?" + (cacheScripts ? "" : d.getTime()),
		//"addVotingView": "../user-defined/views/backbone.view.addvoting.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingResultView": "../user-defined/views/backbone.view.votingresult.js?" + (cacheScripts ? "" : d.getTime()),
		//"openQuestionView": "../user-defined/views/backbone.view.openquestion.js?" + (cacheScripts ? "" : d.getTime()),
		//"fixedQuestionView": "../user-defined/views/backbone.view.fixedquestion.js?" + (cacheScripts ? "" : d.getTime()),
		//"addQuestionView": "../user-defined/views/backbone.view.addquestion.js?" + (cacheScripts ? "" : d.getTime()),


		//"page": "../user-defined/pages/backbone.page.js?" + (cacheScripts ? "" : d.getTime()),
		//"pageCollection": "../user-defined/pages/backbone.page.collection.js?" + (cacheScripts ? "" : d.getTime()),
		//"mainPage": "../user-defined/pages/backbone.page.main.js?" + (cacheScripts ? "" : d.getTime()),
		//"adminVotingsPage": "../user-defined/pages/backbone.page.adminvotings.js?" + (cacheScripts ? "" : d.getTime()),
		//"userVotingsPage": "../user-defined/pages/backbone.page.uservotings.js?" + (cacheScripts ? "" : d.getTime()),
		//"commentsPage": "../user-defined/pages/backbone.page.comments.js?" + (cacheScripts ? "" : d.getTime()),
		//"profilePage": "../user-defined/pages/backbone.page.profile.js?" + (cacheScripts ? "" : d.getTime()),
		//"usersPage": "../user-defined/pages/backbone.page.users.js?" + (cacheScripts ? "" : d.getTime()),
		//"voicesPage": "../user-defined/pages/backbone.page.voices.js?" + (cacheScripts ? "" : d.getTime()),
		//"votingPage": "../user-defined/pages/backbone.page.voting.js?" + (cacheScripts ? "" : d.getTime()),
	},
	"shim": {
		"angular": {
			exports: "angular"
		},
		"angular.route": { deps: ["angular"] },
		"bootstrap": {
			deps: ["jquery"]
		},
		"bootstrapPaginator": {
			deps: ["jquery", "bootstrap"]
		},
		"bootstrapValidator": {
			deps: ["jquery", "bootstrap"]
		},
		"bootpag": {
			deps: ["jquery"]
		},
		"underscore": {
			exports: "_"
		},
		"signalr": {
			deps: ["jquery"],
			exports: "$.connection"
		},
		"signalr.hubs": {
			deps: ["signalr"],
		},
		"icheck": {
			deps: ["jquery"]
		}
	},
	waitSeconds: 0
});

require(["user-defined/angular.app"]);