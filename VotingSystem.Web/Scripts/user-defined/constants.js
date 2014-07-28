define(["underi18n"], function () {
	var constants = {
		"answerSavedMessage": "Your answer was saved.",
		
		"canUseLoginMessage": "You can use this username.",
		"commentSavedMessage": "Comment was successfully added.",
		"commentLengthMessage": "Comment lenght can't be less than 4 characters.",
		"commentDeletedMessage": "Comment was successfully deleted.",
		"commentAddedMessage": "New comment was added.",
		"commentDeletedTemplateMessage": "Comment \"{0}\" was deleted.",

		"dontHaveCommentsMessage": "You don't have comments.",
		"dontHaveVoicesMessage": "You don't have voices.",
		"dontHaveVotingsMessage": "You don't have votings.",

		"emptyAnswerMesage": "Please enter answer.",
		"emptyFixedAnswerMessage": "Please specify answer text for this predefined answer.",
		"errorOccurredDuringDeletingProcessMessage": "An error occurred during deleting process.",
		"errorOccurredDuringSavingProcessMessage": "An error occurred during saving process.",

		"invalidAnswerMessage": "Please fill all fields and continue.",

		"loadResultFailedMessage": "Failed to load voting results.",
		"loginAlreadyExistsMessage": "This username already exists.",
		"loginFailedMessage": "Login failed.",
		"logOutFailedMessage": "Logout failed.",

		"passwordChangedMessage": "The password was successfully updated.",

		"registrationFailedMessage": "Registration failed.",
		"registrationSucceedMessage": "Please check your e-mail to complete registration.",

		"unknownErrorOnLoadingDataMesage": "An unknown error occurred when data loading.",
		"userDeletedMessage": "User was successfully deleted.",
		"userImageUpdatedMessage": "The image was successfully updated.",
		"userLockChangedMessage": "User lock was successfully changed.",
		"userLockChangeFailedMessage": "Change lock failed.",
		"userPrivacySettingUpdatedMessage": "The privacy settings were successfully updated.",
		"userRolesChangedMessage": "Roles were successfully changed.",
		"userRolesOneRoleShouldBeSelectedMessage": "At least one role should be selected.",
		"usersNotFoundMessage": "Users not found.",
		"userSuggestedToBlockMessage": "This user was suggested to block.",
		"userUnSuggestMessage": "User was successfully unsuggested.",

		"votingCreatedMessage": "New voting successfully created.",
		"votingCreateValidateMessage": "Field \"{0}\" is required.",
		"votingCreateValidateDateMessage": "\"End Date\" must be greater or equal to \"Start Date\"",
		"votingCreateValidateTypeMessage": "Questions of \"Choice Question\" type should contain at least 2 predefined answeres.",
		"votingCreateValidateQuestionsCountMessage": "Theme should contain at least 1 question.",
		"votingDeletedMessage": "Voting was sucessfully deleted.",
		"votingsNotFoundMessage": "Votings not found.",
		"votingStatusChangedMessage": "Voting status was successfully updated.",
	};

	return underi18n.MessageFactory(constants);
});