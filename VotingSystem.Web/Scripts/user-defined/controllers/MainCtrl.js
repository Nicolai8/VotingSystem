angular.module("votingSystem.controllers.main", [])
	.controller("MainCtrl", ["$scope", "$routeParams", "UnitOfWork", "commentsHub",
		function ($scope, $routeParams, UnitOfWork, commentsHub) {
			$scope.page = $routeParams.pageNumber;
			$scope.breadCrumbItemName = "";
			$scope.pageName = "main";
			$scope.total = 1;
			$routeParams.searchQuery = $routeParams.searchQuery ? $routeParams.searchQuery : "";
			$scope.searchQuery = $routeParams.searchQuery;

			commentsHub.changePageOnHub("");

			UnitOfWork.votingStorage().query(
				{
					pageType: "MainPage",
					page: $routeParams.pageNumber,
					query: $routeParams.searchQuery
				},
				function (data) {
					$scope.votings = data;
					UnitOfWork.votingStorage().total(
					{
						totalKind: "totalActive",
						query: $routeParams.searchQuery
					},
					function (response) {
						$scope.total = response.total;
					});
				});
		}]);

