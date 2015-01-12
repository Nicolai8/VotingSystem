using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/answer")]
	public class AnswerApiController : BaseApiController
	{
		private readonly IAnswerService _answerService;
		private readonly IUserProfileService _userProfileService;
		private readonly IThemeService _themeService;

		public AnswerApiController(IAnswerService answerService, IUserProfileService userProfileService, IThemeService themeService)
		{
			_answerService = answerService;
			_userProfileService = userProfileService;
			_themeService = themeService;
		}

		#region Public Methods
		[Route("")]
		[CustomAuthorizeApi]
		public IEnumerable<AnswerModel> Get(int page, int size)
		{
			List<Answer> answers = _answerService.GetByUserId(UserId, page, size);
			UserProfile userProfile = _userProfileService.GetByUserId(UserId);
			string pictureUrl = userProfile != null && !String.IsNullOrEmpty(userProfile.PictureUrl) ?
				userProfile.PictureUrl : GlobalVariables.DefaultImagePath;
			return answers.Select(a => a.ToAnswerModel(pictureUrl));
		}

		[Route("{id:int}")]
		public IEnumerable<QuestionModel> Get(int id)
		{
			int userId = User.Identity.IsAuthenticated ? UserId : -1;
			Theme theme = _themeService.GetThemeById(id);
			return theme.Questions.Select(q => q.ToQuestionModel(userId));
		}

		[Route("")]
		[CustomAuthorizeApi]
		public int Post([FromBody]IEnumerable<Answer> answers)
		{
			return Vote(answers, UserId);
		}

		[Route("total", Name = "TotalAnswers")]
		[HttpGet]
		[CustomAuthorizeApi]
		public int GetTotalAnswers()
		{
			return _answerService.GetNumberOfUserAnswers(UserId);
		}

		[Route("")]
		public int Post([FromBody]IEnumerable<Answer> answers, [FromUri]int captcha)
		{
			int validCaptcha = GlobalVariables.Captcha;
			if (validCaptcha == captcha)
			{
				return Vote(answers, null);
			}
			throw new VotingSystemException("Captcha is invalid");
		}
		#endregion

		#region Private methods

		private int Vote(IEnumerable<Answer> answers, int? userId)
		{
			int themeId = _themeService.GetThemeByQuestionId(answers.First().QuestionId).Id;
			if (userId != null && _answerService.IsThemeAnswered(themeId, userId.Value))
			{
				throw new VotingSystemException("You already answered on this voting.");
			}
			if (!_themeService.IsClosed(themeId))
			{
				_answerService.AddAnswer(answers, userId);
				List<Answer> answersToTheme = _answerService.GetByThemeId(themeId, 1, int.MaxValue);
				return answersToTheme.Count(a => a.QuestionId == answersToTheme.First().QuestionId);
			}
			throw new VotingSystemException("Voting is closed or blocked.");
		}

		#endregion
	}
}