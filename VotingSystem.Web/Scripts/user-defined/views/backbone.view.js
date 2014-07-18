define(["backbone", "templates"],
	function (Backbone, templates) {
		var view = Backbone.View.extend({
			tagName: "div",
			className: "col-md-12",

			initialize: function (model) {
				this.model = model;
				this.model.on("destroy", this.unrender, this);
				this.defineCustomBind();
			},
			render: function () {
				this.$el.html(templates.get(this.templateName)(this.model.toJSON()));
				this.afterRender();
				return this;
			},
			afterRender: function () {
			},
			unrender: function () {
				this.undelegateEvents();
				this.$el.removeData().unbind().remove();
			},
			defineCustomBind: function () {
			}
		});
		return view;
	});