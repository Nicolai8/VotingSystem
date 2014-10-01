define(["angular", "signalr", "signalr.hubs"], function (angular) {
	angular.module("votingSystem.services.commentsHub", [])
		.service("commentsHub", function () {
			return {
				stopCommentsHub: function () {
					$.connection.hub.stop();
				},
				startCommentsHub: function () {
					return $.connection.hub.start();
				},
				resetCommentsHub: function (afterResetHandler) {
					var that = this;
					this.startCommentsHub().done(function () {
						that.stopCommentsHub();
						afterResetHandler();
					});
				},
				changePageOnHub: function (votingId) {
					var that = this;
					if ($.connection.hub.state == $.signalR.connectionState.connected) {
						votingId = angular.isDefined(votingId) ? votingId : "";
						$.connection.commentsHub.server.changePage(votingId);
					} else {
						this.resetCommentsHub(function () {
							$.connection.hub.start().done(function () {
								that.changePageOnHub(votingId);
							});
						});

					}
				},
				setCreateCommentHandler: function (createCommentHandler) {
					$.connection.commentsHub.client.createComment = createCommentHandler;
				},
				setDeleteCommentHandler: function (deleteCommentHandler) {
					if (!angular.isDefined($.connection.commentsHub.client.deleteComment)) {
						$.connection.commentsHub.client.deleteComment = deleteCommentHandler;
					}
				},
				createComment: function (comment) {
					$.connection.commentsHub.server.createComment(comment);
				},
				deleteComment: function (comment) {
					$.connection.commentsHub.server.deleteComment(comment.CommentId, comment.VotingId);
				}
			};
		});
});

