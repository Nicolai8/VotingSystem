define(["backbone"],
function (Backbone) {
	return Backbone.Model.extend({
		defaults: {
			page: 1,
			pageSize: 10,
			pageName: "",
			query: "",
			votingId: 1,
			userName: "",
			loggedUserName: "",
		},
		prev: { pageName: "", page: 1, query: "", userName: "", roles: "" },

		defineCustomBind: function (attribute, watchValue) {
			this.on("change:" + attribute + " change:page change:query change:userName", function (model, val, options) {
				if (watchValue === model.get("pageName")) {
					if (model.prev.pageName != model.changed.pageName
						|| model.prev.page != model.changed.page
						|| model.prev.query != model.changed.query
						|| model.prev.userName != model.changed.userName) {
						
						model.trigger("change:" + attribute + "=" + watchValue, model, watchValue, options);
						model.prev.pageName = model.changed.pageName;
						model.prev.page = model.changed.page;
						model.prev.query = model.changed.query;
						model.prev.userName = model.changed.userName;
					}
				}
			});
		},
	});
});