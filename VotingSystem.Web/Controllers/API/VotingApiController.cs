using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Structures;
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
			Theme theme = _themeService.GetThemeById(id);
			if (theme != null)
			{
				string userName = string.Empty;
				bool isAnswered = false;
				if (User.Identity.IsAuthenticated)
				{
					userName = User.Identity.Name;
					isAnswered = _answerService.IsThemeAnswered(id, UserId);
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
			Filter<Theme> filter = new Filter<Theme>(null, page, size); 

			switch (pageType)
			{
				case PageType.UserVotings:
					themes = _themeService.GetThemesByUserId(query, UserId, filter);
					break;
				case PageType.AdminVotings:
					if (!User.IsInRole(RoleType.Admin.ToString()) && !User.IsInRole(RoleType.Moderator.ToString()))
					{
						throw new AuthenticationException();
					}
					themes = _themeService.GetAllThemes(query, filter);
					break;
				default:
					themes = _themeService.GetAllActiveThemes(query, filter);
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
		[System.Web.Http.HttpGet]
		public int GetTotalActiveVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetNumberOfActiveThemesByThemeName(query);
		}

		[System.Web.Http.HttpGet]
		[Route("totaluser", Name = "TotalUser")]
		[CustomAuthorizeApi]
		public int GetTotalUserVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetNumberOfUserThemes(UserId, query);
		}

		[System.Web.Http.HttpGet]
		[Route("totaladmin", Name = "TotalAdmin")]
		[CustomAuthorizeApi(Roles = new[] { RoleType.Admin, RoleType.Moderator })]
		public int GetTotalAdminVotings(string query = "")
		{
			if (String.IsNullOrEmpty(query))
			{
				query = "";
			}
			return _themeService.GetNumberOfThemesByThemeName(query);
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
				_themeService.InsertTheme(theme);

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
			_themeService.DeleteTheme(id);
		}
	}
}