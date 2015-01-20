angular.module("votingSystem.directives.fileUploader", [])
	.directive("fileUploader",["constants","urls", function (constants, urls) {
		return {
			scope: { pictureUrl: "=" },
			link: function (scope, elem, attrs) {
				var $progress = $("#progress");
				angular.element(elem).fileupload({
					url: urls.ProfilePage.ChangePicture,
					done: function (e, data) {
						$progress.hide();
						toastr.success(constants["userImageUpdatedMessage"]);
						scope.pictureUrl = data.result;
						scope.$apply();
					},
					progressall: function (e, data) {
						$progress.show();
						var progress = parseInt(data.loaded / data.total * 100, 10);
						$progress.find(".progress-bar").css(
						"width",
						progress + "%"
						);
					},
					fail: function () {
						$progress.hide();
						toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
					}
				});
			}
		};
	}]);