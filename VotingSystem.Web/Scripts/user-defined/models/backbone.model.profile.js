define(["userModel", "Urls"],
function (UserModel, Urls) {
	return UserModel.extend({
		methodUrl: {
			"delete": function (model) {
				return Urls.Users + "/" + model.get("UserName");
			},
			"update": function (model) {
				return Urls.Users + "/" + model.get("UserName");
			}
		},

		url: function () {
			return Urls.ProfilePage.GetProfile + "?username=" + window.state.get("userName");
		}
	});
});