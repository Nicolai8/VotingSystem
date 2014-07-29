define(["angular"], function (angular) {
	angular.module("votingSystem.directives.pieChart", [])
		.directive("pieChart", function () {
			return function (scope, elem, attrs) {
				var arrayOfResults = JSON.parse(attrs.answers);
				arrayOfResults.unshift(["Answer", "Count"]);
				var options = {
					title: attrs.title,
					height: 400,
					width: 700,
					sliceVisibilityThreshold: 5 / 360,
					pieSliceText: "none",
					titleTextStyle: {
						fontSize: 20
					}
				};

				if (attrs.answerIndex != -1) {
					options.slices = {};
					options.slices[Number.parseInt(attrs.answerIndex)] = { offset: 0.2 };
				}

				var data = google.visualization.arrayToDataTable(arrayOfResults);
				new google.visualization.PieChart(angular.element(elem)[0]).draw(data, options);
			};
		});
});