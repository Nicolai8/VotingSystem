define(["angular",
	"directives/angular.directive.bootstrapmarkdown",
	"directives/angular.directive.datetimepicker",
	"directives/angular.directive.focusout",
	"directives/angular.directive.goback",
	"directives/angular.directive.notfound",
	"directives/angular.directive.onenterpress",
	"directives/angular.directive.paginator",
	"directives/angular.directive.piechart",
	"directives/angular.directive.trusthtml",
	"directives/angular.directive.validatefield",
	"directives/angular.directive.validateform"
	],
	function (angular) {
		angular.module("votingSystem.directives", [
			"votingSystem.directives.bootstrapMarkdown",
			"votingSystem.directives.datetimePicker",
			"votingSystem.directives.focusOut",
			"votingSystem.directives.goBack",
			"votingSystem.directives.notFound",
			"votingSystem.directives.onEnterPress",
			"votingSystem.directives.votingPaginator",
			"votingSystem.directives.pieChart",
			"votingSystem.directives.trustHtml",
			"votingSystem.directives.validateField",
			"votingSystem.directives.validateForm"
		]);

	});
