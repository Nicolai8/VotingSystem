using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/voting")]
	public class VotingApiController : BaseApiController
	{
		private readonly IThemeService _themeService;
		private readonly IAnswerService _answerService;

		public VotingApiController(IThemeService themeService, IAnswerService answerService)
		{
			_themeService = themeService;
			_answerService = answerService;
		}

		[Route("{id:int}")]
		public VotingPageModel Get(int id)
		{
			Theme theme = _themeService.GetByThemeId(id);
			if (theme != null)
			{
				string userName = string.Empty;
				bool isAnswered = false;
				if (User.Identity.IsAuthenticated)
				{
					userName = User.Identity.Name;
					isAnswered = _answerService.IsAnswered(id, UserId);
				}

				return theme.ToVotingPageModel(isAnswered, userName);
			}

			throw new VotingSystemException("Voting not found.");
		}

		[Route("{pageType}/{page:int}/{query?}")]
		public IEnumerable<VotingModel> Get(PageType pageType, int page = 1, int size = 10, string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			List<Theme> themes;
			switch (pageType)
			{
				case PageType.UserVotings:
					themes = _themeService.GetByUserId(query, UserId, null, page, size);
					break;
				case PageType.AdminVotings:
					if (!User.IsInRole(RoleType.Admin.ToString()) && !User.IsInRole(RoleType.Moderator.ToString()))
					{
						throw new AuthenticationException();
					}
					themes = _themeService.GetAll(query, null, page, size);
					break;
				default:
					themes = _themeService.GetAllActive(query, null, page, size);
					break;
			}

			List<VotingModel> model = themes.Select(t =>
				{
					MembershipUser membershipUser = Membership.GetUser(t.UserId ?? -1);
					return t.ToVotingModel(membershipUser != null ? membershipUser.UserName : string.Empty);
				}).ToList();
			return model;
		}

		[Route("totalactive", Name = "TotalActive")]
		[HttpGet]
		public int GetTotalActiveVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetTotalActive(query);
		}

		[HttpGet]
		[Route("totaluser", Name = "TotalUser")]
		[CustomAuthorizeApi]
		public int GetTotalUserVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetTotal(UserId, query);
		}

		[HttpGet]
		[Route("totaladmin", Name = "TotalAdmin")]
		[CustomAuthorizeApi(Roles = new[] { RoleType.Admin, RoleType.Moderator })]
		public int GetTotalAdminVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetTotal(query);
		}

		[CustomAuthorizeApi]
		[Route("")]
		public void Post([FromBody]Theme theme)
		{
			if (ModelState.IsValid)
			{
				theme.UserId = UserId;
				theme.CreateDate = DateTime.Now;
				theme.Status = StatusType.NotApproved;
				_themeService.Insert(theme);

				return;
			}
			throw new VotingSystemException("Create new voting failed.");
		}

		[Route("{id:int}")]
		[OwnsApi(OwnsParameter = OwnsType.Theme)]
		public VotingModel Put(int id, [FromBody]VotingModel theme)
		{
			_themeService.UpdateStatus(id, theme.Status.Value);
			return theme;
		}

		[Route("{id:int}")]
		[OwnsApi(OwnsParameter = OwnsType.Theme)]
		public void Delete(int id)
		{
			_themeService.Delete(id);
		}
	}
}