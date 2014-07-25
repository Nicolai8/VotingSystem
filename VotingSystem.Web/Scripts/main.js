requirejs.config({
	"baseUrl": "/VotingSystem.Web/scripts/libraries",
	"paths": {
		"user-defined": "../user-defined",
		"controllers": "../user-defined/controllers",
		"directives": "../user-defined/directives",
		"services": "../user-defined/services",
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
		"angular": "angular/angular",
		"angular.route": "angular/angular-route",
		"angular.resource": "angular/angular-resource",
	},
	"shim": {
		"angular": {
			exports: "angular"
		},
		"angular.route": { deps: ["angular"] },
		"angular.resource": { deps: ["angular"] },
		"bootstrap": { deps: ["jquery"] },
		"bootstrapPaginator": { deps: ["jquery", "bootstrap"] },
		"bootstrapValidator": { deps: ["jquery", "bootstrap"] },
		"bootpag": { deps: ["jquery"] },
		"underscore": {
			exports: "_"
		},
		"signalr": {
			deps: ["jquery"],
			exports: "$.connection"
		},
		"signalr.hubs": { deps: ["signalr"], },
		"icheck": { deps: ["jquery"] }
	},
	waitSeconds: 0
});

require(["user-defined/angular.app"]);