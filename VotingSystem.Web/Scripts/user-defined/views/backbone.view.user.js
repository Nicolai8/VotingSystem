define(["jquery", "underscore", "view", "Urls", "toastr", "constants", "templates", "bootstrapValidator", "icheck"],
	function ($, _, View, Urls, toastr, constants, templates) {
		var userView = View.extend({
			tagName: "tr",
			className: "",

			templateName: "UserTemplate",

			events: {
				"click .change-roles-open-dialog-button": "changeRolesDialog",
				"click .toggle-lock-user-button": "toggleLockUser",
				"click .remove-user-button": "removeUser",
				"click .unsuggest-user-button": "unsuggestUser"
			},

			defineCustomBind: function () {
				this.model.on("change:Roles change:IsBlocked", this.render, this);
			},

			toggleLockUser: function (e) {
				e.preventDefault();
				this.model.save({ "IsBlocked": !this.model.get("IsBlocked") }, { wait: true })
					.done(function () {
						toastr.success(constants("userLockChangedMessage"));
					}).fail(function () {
						toastr.error(constants("userLockChangeFailedMessage"));
					});
			},
			removeUser: function (e) {
				e.preventDefault();
				var userName = this.model.get("UserName");
				if (confirm("Are you sure that you whan delete " + userName + "?"))
					this.model.destroy({ wait: true })
						.done(function () {
							toastr.success(constants("userDeletedMessage"));
						}).fail(function () {
							toastr.error(constants("errorOccurredDuringDeletingProcessMessage"));
						});
			},
			unsuggestUser: function (e) {
				e.preventDefault();
				var that = this;
				var userName = this.model.get("UserName");
				$.post(Urls.Users + "/" + userName + "/unsuggest")
					.done(function () {
						toastr.success(constants("userUnSuggestMessage"));
						that.collection.CollectionView.render();
					}).fail(function () {
						toastr.error(constants("errorOccurredDuringSavingProcessMessage"));
					});
			},
			changeRolesDialog: function (e) {
				e.preventDefault();
				var $modal = $("#changeRolesModal");
				var $form = $modal.find("form[data-validate-form='true']");
				var userRoles = this.model.get("Roles");
				var $roles = $("#roles");
				$roles.attr("userName", this.model.get("UserName"));
				$roles.find("input").each(function () {
					var $this = $(this);
					if(!$this.data("default")){
						$form.bootstrapValidator('removeField', $(this));
						$this.closest(".checkbox").remove();
					}
				});
				$.get(Urls.UsersPage.GetAllRoles)
					.done(function (data) {
						var roleTemplate = templates.get("RoleTemplate");
						$.each(data, function (index, value) {
							var isInRole = $.inArray(value, userRoles) != -1;
							var $role = $(roleTemplate({ IsInRole: isInRole, RoleName: value }));
							$roles.append($role);
							$role.iCheck({
								checkboxClass: "icheckbox_flat-red",
							}).on('ifChanged', function (e) {
								var field = $(this).attr('name');
								$form.bootstrapValidator('revalidateField', field);
							});
							$form.bootstrapValidator('addField', $role.find("input"));
						});
						
						$modal.modal("show");
					});
			},
		});
		return userView;
	});