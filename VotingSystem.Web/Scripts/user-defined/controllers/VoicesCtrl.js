angular.module("votingSystem.controllers.voices", [])
	.controller("VoicesCtrl", ["$scope", "$routeParams", "UnitOfWork", "commentsHub",
		function ($scope, $routeParams, UnitOfWork, commentsHub) {
			$scope.pageName = "voices";
			$scope.total = 1;
			$scope.breadCrumbItemName = "Voices";

			commentsHub.changePageOnHub();

			UnitOfWork.voiceStorage().query({ page: $routeParams.pageNumber },
				function (voices) {
					$scope.voices = voices;
					UnitOfWork.voiceStorage().total(
						function (response) {
							$scope.total = response.total;
						});
				});
		}]);

