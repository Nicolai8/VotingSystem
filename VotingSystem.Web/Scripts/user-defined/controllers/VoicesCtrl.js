angular.module("votingSystem.controllers.voices", [])
    .controller("VoicesCtrl", ["$scope", "$http", "$route", "$routeParams", "$location", "constants", "reload", "VoiceStorage", "commentsHub",
        function ($scope, $http, $route, $routeParams, $location, constants, reload, VoiceStorage, commentsHub) {
            $scope.page = $routeParams.pageNumber;
            $scope.pageName = "voicespage";
            $scope.total = 1;
            $scope.constants = constants;
            $scope.$location = $location;
            $scope.$route = $route;
            $scope.reload = reload;
            $scope.breadCrumbItemName = "Voices";

            commentsHub.changePageOnHub();

            VoiceStorage.query({ page: $scope.page },
                function (voices) {
                    $scope.voices = voices;
                    VoiceStorage.total(
                        function (response) {
                            $scope.total = response.total;
                        });
                });
        }]);

