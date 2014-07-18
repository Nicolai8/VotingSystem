define(["model", "Urls", "toastr", "constants"],
function (Model, Urls, toastr, constants) {
	return Model.extend({
		idAttribute: "UserName",

		validate: function (attrs, options) {
			if (options.skipValidation) {
				return;
			}
			
			if (attrs.Roles.length == 0) {
				toastr.warning(constants("userRolesOneRoleShouldBeSelectedMessage"));
				return constants("userRolesOneRoleShouldBeSelectedMessage");
			}
		},

		urlRoot: Urls.Users
	});
});