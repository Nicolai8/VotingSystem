using System.Web.Http.Controllers;
using System.Web.Mvc;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Helpers;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace VotingSystem.Web.Filters
{
	public class OwnsApiAttribute : AuthorizeAttribute
	{
		private readonly IVotingService _votingService;
		private readonly ICommentService _commentService;
		public OwnsType OwnsParameter { get; set; }

		public OwnsApiAttribute()
		{
			_votingService = DependencyResolver.Current.GetService<IVotingService>();
			_commentService = DependencyResolver.Current.GetService<ICommentService>();
		}

		protected override bool IsAuthorized(HttpActionContext filterContext)
		{
			bool isAuthorized = false;
			string id = filterContext.ControllerContext.RouteData.Values["id"].ToString();

			switch (OwnsParameter)
			{
				case OwnsType.Voting:
					isAuthorized = AuthorizeAttributeHelper.Owns(id, votingId => _votingService.GetVotingById(votingId), AuthorizeAttributeHelper.GetUserIdFromVoting);
					break;
				case OwnsType.Comment:
					isAuthorized = AuthorizeAttributeHelper.Owns(id, _commentService.GetCommentById, AuthorizeAttributeHelper.GetUserIdFromComment);
					break;
			}

			return AuthorizeAttributeHelper.IsAuthorizedLog(isAuthorized);
		}
	}
}