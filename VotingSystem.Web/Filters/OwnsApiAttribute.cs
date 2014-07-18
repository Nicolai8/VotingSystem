using System.Web.Http.Controllers;
using System.Web.Mvc;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace VotingSystem.Web.Filters
{
	public class OwnsApiAttribute : AuthorizeAttribute
	{
		private readonly IThemeService _themeService;
		private readonly ICommentService _commentService;
		public OwnsType OwnsParameter { get; set; }

		public OwnsApiAttribute()
		{
			_themeService = DependencyResolver.Current.GetService<IThemeService>();
			_commentService = DependencyResolver.Current.GetService<ICommentService>();
		}

		protected override bool IsAuthorized(HttpActionContext filterContext)
		{
			bool isAuthorized = false;
			string id = filterContext.ControllerContext.RouteData.Values["id"].ToString();
			switch (OwnsParameter)
			{
				case OwnsType.Theme:
					isAuthorized = AuthorizeAttributeHelper.Owns(id, _themeService.GetByThemeId, AuthorizeAttributeHelper.GetUserIdFromTheme);
					break;
				case OwnsType.Comment:
					isAuthorized = AuthorizeAttributeHelper.Owns(id, _commentService.GetByCommentId, AuthorizeAttributeHelper.GetUserIdFromComment);
					break;
			}
			return AuthorizeAttributeHelper.IsAuthorizedLog(isAuthorized);
		}
	}
}