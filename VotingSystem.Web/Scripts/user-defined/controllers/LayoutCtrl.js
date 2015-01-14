angular.module("votingSystem.controllers.layout", [])
    .controller("LayoutCtrl", ["$scope", "$route", "$routeParams", "$location", "urls", "constants", "commentsHub",
        function ($scope, $route, $routeParams, $location, urls, constants, commentsHub) {
            $scope.accountName = window.loggedUserName;
            $scope.roles = [];
            $scope.authenticated = window.authenticated;
            $scope.$location = $location;
            $scope.commentsHub = commentsHub;

            $scope.logIn = function () {
                var userName = $("#userName").val();
                var password = $("#password").val();
                var $form = $("#loginForm form[data-validate-form]").data("bootstrapValidator");
                if ($form.isValid()) {
                    $.ajax({
                        url: urls.LoginPage.Login,
                        type: "POST",
                        data: { userName: userName, password: password, rememberMe: false },
                        beforeSend: function () {
                            $scope.commentsHub.stopCommentsHub();
                        }
                    }).done(function (data) {
                        if (data && data.result) {
                            $scope.authenticated = true;
                            $scope.accountName = userName;
                            $scope.$apply();
                            $("#loginForm").modal("hide");
                            $scope.$location.path("/mainpage/1");
                        } else {
                            toastr.error(constants["loginFailedMessage"]);
                        }
                    }).fail(function () {
                        toastr.error(constants["loginFailedMessage"]);
                    }).always(function () {
                        //commentsHub.changePageOnHub("");
                        $("#password").val("");
                    });
                } else {
                    $form.validate();
                }
            };

            $scope.$watch("authenticated", function (newValue) {
                if (newValue) {
                    $.get(urls.IsInRole).done(function (data) {
                        $scope.roles = data.toLowerCase().split(",");
                        $scope.$apply();
                    });
                }
            });

            $scope.checkUserName = function () {
                $.get(urls.LoginPage.CheckUserName, { userName: $scope.registerUserName })
                    .done(function (data) {
                        if (!data.result) {
                            toastr.success(constants["canUseLoginMessage"]);
                        } else {
                            toastr.warning(constants["loginAlreadyExistsMessage"]);
                        }
                    }).fail(function () {
                        toastr.warning(constants["loginAlreadyExistsMessage"]);
                    });
            };

            $scope.register = function () {
                var $form = $("#registerInnerForm").data("bootstrapValidator");
                if ($form.isValid()) {
                    var userName = $scope.registerUserName;
                    var email = $scope.registerEmail;
                    $.post(urls.LoginPage.Register, { newUserName: userName, email: email })
                        .done(function () {
                            $("#registerForm").modal("hide");
                            toastr.success(constants["registrationSucceedMessage"]);
                        }).fail(function () {
                            toastr.error(constants["registrationFailedMessage"]);
                        });
                } else {
                    $form.validate();
                }
            };

            $scope.signOut = function ($event) {
                $event.preventDefault();
                $.ajax({
                    url: urls.LoginPage.LogOff,
                    type: "POST",
                    beforeSend: function () {
                        $scope.commentsHub.stopCommentsHub();
                    }
                }).done(function () {
                    $scope.authenticated = false;
                    $scope.accountName = "Account";
                    $scope.$location.path("/mainpage/1");
                    $scope.$apply();
                }).fail(function () {
                    toastr.error(constants["logOutFailedMessage"]);
                }).complete(function () {
                    //commentsHub.changePageOnHub("");
                });
            };

            $("body").on("hidden.bs.modal", "[role='dialog']", function (e) {
                var $modal = $(e.currentTarget);
                $modal.find(":text,textarea,:password").each(function () {
                    $(this).val("");
                });
                $modal.find(":checked").each(function () {
                    $(this).attr("checked", "false");
                });
                $modal.find(".wmd-preview").empty();
                $modal.find("form[data-validate-form]").each(function () {
                    var validateForm = $(this).data("bootstrapValidator");
                    if (angular.isDefined(validateForm)) {
                        validateForm.resetForm();
                    }
                });
            });

            var opts = {
                lines: 10,
                length: 15,
                width: 8,
                radius: 20,
                trail: 60,
            };
            var spinner = new Spinner(opts).spin($("#preloader")[0]);
        }]);