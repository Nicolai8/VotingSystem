using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;
using AuthorizeAttribute = Microsoft.AspNet.SignalR.AuthorizeAttribute;

namespace VotingSystem.Web.Filters
{
	public class OwnsHubAttribute : AuthorizeAttribute
	{
		private readonly IThemeService _themeService;
		private readonly ICommentService _commentService;
		public OwnsType OwnsParameter { get; set; }

		public OwnsHubAttribute()
		{
			_themeService = DependencyResolver.Current.GetService<IThemeService>();
			_commentService = DependencyResolver.Current.GetService<ICommentService>();
		}

		protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
		{
			bool isAuthorized = false;
			string id = HttpContext.Current.Request.Form["data"];
			if (!string.IsNullOrEmpty(id))
			{
				id = Regex.Match(id, @"\""A\"":\[\""(\d+)\""\]").Groups[1].Value;
				switch (OwnsParameter)
				{
					case OwnsType.Theme:
						isAuthorized = AuthorizeAttributeHelper.Owns(id, _themeService.GetByThemeId, AuthorizeAttributeHelper.GetUserIdFromTheme);
						break;
					case OwnsType.Comment:
						isAuthorized = AuthorizeAttributeHelper.Owns(id, _commentService.GetByCommentId, AuthorizeAttributeHelper.GetUserIdFromComment);
						break;
				}
			}
			return AuthorizeAttributeHelper.IsAuthorizedLog(isAuthorized);
		}
	}
}