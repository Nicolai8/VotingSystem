define(["angular", "Urls", "constants", "toastr", "bootpag", "angular.route"],
	function (angular, Urls, constants, toastr) {
		return function (controllersModule) {
			controllersModule
				.controller('AdminVotingsController', function ($scope, votingStorage, $reload, $http, $route, $routeParams, $location) {
					$scope.page = $routeParams.pageNumber;
					$scope.pageName = "adminvotingspage";
					$scope.total = 1;
					$scope.constants = constants;
					$scope.$location = $location;
					$scope.$route = $route;
					$scope.reload = $reload;

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
						var oldStatus = angular.copy(voting.Status);
						voting.Status = status;
						voting.$update().then(
							function () {
								toastr.success(constants("votingStatusChangedMessage"));
							}, function () {
								voting.Status = oldStatus;
								toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
							});
					};

					$scope.removeTheme = function (voting) {
						voting.$remove(
							function () {
								$scope.votings.splice($scope.votings.indexOf(voting), 1);
								toastr.success(constants("votingDeletedMessage"));
								$scope.reload($scope, "/adminvotingspage/{pageNumber}");
							},
							function () {
								toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
							});
					};

					
				});
		};
	});

