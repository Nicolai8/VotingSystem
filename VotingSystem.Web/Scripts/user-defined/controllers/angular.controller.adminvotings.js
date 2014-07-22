define(["angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function (angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller('AdminVotingsController', function ($scope, votingStorage, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "adminvotingspage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;

					votingStorage.query(
						{
							pageType: "AdminVotings",
							page: $scope.page
						},
						function (data) {
							$scope.votings = data;
							votingStorage.total(
								{
									 totalKind: "totaladmin"
								},
								function (response) {
									$scope.total = response.total;
								});
						});

					$scope.setThemeStatus = function (voting, status) {
						votingStorage.put(
							{
								 id: voting.VotingId
							},
							{
								 "Status": status
							},
							function () {
								toastr.success(constants("votingStatusChangedMessage"));
								voting.Status = status;
							}, function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};

					$scope.removeTheme = function (voting) {
						votingStorage.delete(
							{
								id: voting.VotingId
							},
							function () {
								$scope.votings.splice($scope.votings.indexOf(voting), 1);
								toastr.success(constants("votingDeletedMessage"));
								$scope.render();
							},
							function () {
								toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
							});
					};

					$scope.render = function () {
						var pageNumber = $scope.page;
						if (pageNumber != 1) {
							if (votings.length == 0) {
								pageNumber -= 1;
							}
						}
						if (pageNumber != $scope.page) {
							$scope.$location.path("/adminvotingspage/" + pageNumber);
						} else {
							$route.reload();
						}
					};
				});
		};
	});

