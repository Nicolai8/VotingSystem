angular.module("votingSystem.controllers.adminVotings", [])
    .controller("AdminVotingsCtrl", ["$scope", "$http", "$route", "$routeParams", "$location", "constants", "reload", "VotingStorage", "commentsHub",
        function ($scope, $http, $route, $routeParams, $location, constants, reload, VotingStorage, commentsHub) {
            $scope.page = $routeParams.pageNumber;
            $scope.breadCrumbItemName = "Admin Votings";
            $scope.pageName = "adminvotingspage";
            $scope.total = 1;
            $scope.constants = constants;
            $scope.$location = $location;
            $scope.$route = $route;
            $scope.reload = reload;

            commentsHub.changePageOnHub();

            VotingStorage.query(
                {
                    pageType: "AdminVotings",
                    page: $scope.page
                },
                function (data) {
                    $scope.votings = data;
                    VotingStorage.total(
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
                        toastr.success(constants["votingStatusChangedMessage"]);
                    }, function () {
                        voting.Status = oldStatus;
                        toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
                    });
            };

            $scope.removeTheme = function (voting) {
                voting.$remove(
                    function () {
                        $scope.votings.splice($scope.votings.indexOf(voting), 1);
                        toastr.success(constants["votingDeletedMessage"]);
                        $scope.reload($scope, $scope.votings.length, "/" + $scope.pageName + "/{pageNumber}");
                    },
                    function () {
                        toastr.error(constants["errorOccurredDuringDeletingProcessMessage"]);
                    });
            };
        }]);