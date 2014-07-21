define(["angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function (angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller('AdminVotingsController', function ($scope, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "adminvotingspage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;

					$http.get(Urls.Votings + "/AdminVotings/" + $routeParams.pageNumber).success(function (data) {
						$scope.votings = data;
						$http.get(Urls.AdminPage.GetTotal).success(function (total) {
							$scope.total = total;
						});
					});

					$scope.setThemeStatus = function (voting, status) {
						$http.put(Urls.Votings + "/" + voting.VotingId, { "Status": status })
							.success(function () {
								toastr.success(constants("votingStatusChangedMessage"));
								voting.Status = status;
							}).error(function () {
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};

					$scope.removeTheme = function (voting) {
						$http.delete(Urls.Votings + "/" + voting.VotingId)
							.success(function () {
								$scope.votings.splice($scope.votings.indexOf(voting), 1);
								toastr.success(constants("votingDeletedMessage"));
								$scope.render();
							}).error(function () {
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

