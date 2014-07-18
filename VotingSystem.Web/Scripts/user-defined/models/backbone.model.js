define(["backbone"],
function (Backbone) {
	return Backbone.Model.extend({
		idAttribute: "Id",
		
		methodUrl: {},

		sync: function (method, model, options) {
			if (model.methodUrl && model.methodUrl[method.toLowerCase()]) {
				options = options || {};
				options.url = model.methodUrl[method.toLowerCase()](model);
			}
			return Backbone.sync(method, model, options);
		}
	});
});