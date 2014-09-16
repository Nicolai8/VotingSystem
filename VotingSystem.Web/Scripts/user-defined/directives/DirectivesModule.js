define(["angular",
	"directives/bootstrapmarkdown",
	"directives/datetimepicker",
	"directives/focusout",
	"directives/goback",
	"directives/notfound",
	"directives/onenterpress",
	"directives/paginator",
	"directives/piechart",
	"directives/trusthtml",
	"directives/validatefield",
	"directives/validateform"
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
