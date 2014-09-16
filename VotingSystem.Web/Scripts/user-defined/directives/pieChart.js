define(["angular"], function (angular) {
	angular.module("votingSystem.directives.pieChart", [])
		.directive("pieChart", function () {
			return {
				scope: {
					answerIndex: "=",
					title: "=",
					answers: "="
				},
				link: function (scope, elem, attrs) {
					var arrayOfResults = scope.answers;
					arrayOfResults.unshift(["Answer", "Count"]);
					var options = {
						title: scope.title,
						height: 400,
						width: 700,
						sliceVisibilityThreshold: 5 / 360,
						pieSliceText: "none",
						titleTextStyle: {
							fontSize: 20
						}
					};

					if (scope.answerIndex != -1) {
						options.slices = {};
						options.slices[Number.parseInt(scope.answerIndex)] = { offset: 0.2 };
					}

					var data = google.visualization.arrayToDataTable(arrayOfResults);
					new google.visualization.PieChart(angular.element(elem)[0]).draw(data, options);
				}
			};
		});
});