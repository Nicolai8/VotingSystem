angular.module("votingSystem.services.notifications", [])
	.service("notifications", ["constants", function (constants) {
		return {
			commentAdded: function () {
				toastr.info(constants["commentAddedMessage"]);
			},
			commentSaved: function () {
				toastr.success(constants["commentSavedMessage"]);
			},
			commentDeleted: function (commentText) {
				if (commentText) {
					toastr.info(constants["commentDeletedTemplateMessage"].replace("{0}", commentText));
				} else {
					toastr.success(constants["commentDeletedMessage"]);
				}
			},

			savingError: function () {
				toastr.error(constants["errorOccurredDuringSavingProcessMessage"]);
			},
			deletingError: function () {
				toastr.error(constants["errorOccurredDuringDeletingProcessMessage"]);
			},

			answerSaved: function () {
				toastr.success(constants["answerSavedMessage"]);
			},
			invalidAnswer: function () {
				toastr.info(constants["invalidAnswerMessage"]);
			},

			votingCreated: function() {
				toastr.success(constants["votingCreatedMessage"]);
			},
			votingDeleted: function () {
				toastr.success(constants["votingDeletedMessage"]);
			},
			votingStatusChanged: function () {
				toastr.success(constants["votingStatusChangedMessage"]);
			},

			loginFailed: function () {
				toastr.error(constants["loginFailedMessage"]);
			},
			canUseLogin: function () {
				toastr.success(constants["canUseLoginMessage"]);
			},
			loginAlreadyExists: function () {
				toastr.warning(constants["loginAlreadyExistsMessage"]);
			},


			registrationSucceed: function () {
				toastr.success(constants["registrationSucceedMessage"]);
			},
			registrationFailed: function () {
				toastr.error(constants["registrationFailedMessage"]);
			},

			logOutFailed: function () {
				toastr.error(constants["logOutFailedMessage"]);
			},

			userLockChanged: function () {
				toastr.success(constants["userLockChangedMessage"]);
			},
			userLockChangeFailed: function () {
				toastr.error(constants["userLockChangeFailedMessage"]);
			},
			userDeleted: function () {
				toastr.success(constants["userDeletedMessage"]);
			},
			passwordChanged: function () {
				toastr.success(constants["passwordChangedMessage"]);
			},
			userUnSuggestToBlock: function () {
				toastr.success(constants["userUnSuggestMessage"]);
			},
			userSuggestedToBlock: function () {
				toastr.success(constants["userSuggestedToBlockMessage"]);
			},
			userPrivacyUpdated: function () {
				toastr.success(constants["userPrivacySettingUpdatedMessage"]);
			},
			userRolesChanged:function() {
				toastr.success(constants["userRolesChangedMessage"]);
			}
		}
	}]);