﻿define(["angular", "Urls", "constants", "angular.route"],
	function (angular, Urls, constants) {
		angular.module("votingSystem.controllers.main", [])
			.controller("MainCtrl", function ($scope, $http, $route, $routeParams, VotingStorage, commentsHub) {
				$scope.page = $routeParams.pageNumber;
				$scope.breadCrumbItemName = '';
				$scope.pageName = "mainpage";
				$scope.total = 1;
				$routeParams.searchQuery = $routeParams.searchQuery ? $routeParams.searchQuery : "";
				$scope.searchQuery = $routeParams.searchQuery;
				$scope.constants = constants;

				commentsHub.changePageOnHub("");

				VotingStorage.query(
					{
						pageType: "MainPage",
						page: $routeParams.pageNumber,
						query: $routeParams.searchQuery
					},
					function (data) {
						$scope.votings = data;
						VotingStorage.total(
							{
								totalKind: "totalactive",
								query: $routeParams.searchQuery
							},
							function (response) {
								$scope.total = response.total;
							});
					});
			});
	});

