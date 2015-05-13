using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.Common.Filters;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;
using VotingSystem.Web.Resources;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/answer")]
	public class AnswerApiController : BaseApiController
	{
		private readonly IAnswerService _answerService;
		private readonly IUserProfileService _userProfileService;
		private readonly IVotingService _votingService;

		public AnswerApiController(IAnswerService answerService, IUserProfileService userProfileService, IVotingService votingService)
		{
			_answerService = answerService;
			_userProfileService = userProfileService;
			_votingService = votingService;
		}

		#region Public Methods

		[Route("")]
		[CustomAuthorizeApi]
		public IEnumerable<AnswerModel> Get(int page, int size)
		{
			List<Answer> answers = _answerService.GetByUserId(UserId, new Filter(page, size));
			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(UserId);
			string pictureUrl = userProfile != null && !String.IsNullOrEmpty(userProfile.PictureUrl) ?
				userProfile.PictureUrl : GlobalVariables.DefaultImagePath;
			return answers.Select(a => a.ToAnswerModel(pictureUrl));
		}

		[Route("{id:int}")]
		public IEnumerable<QuestionModel> Get(int id)
		{
			int userId = User.Identity.IsAuthenticated ? UserId : -1;
			Voting voting = _votingService.GetVotingById(id);
			return voting.Questions.Select(q => q.ToQuestionModel(userId));
		}

		[Route("")]
		[CustomAuthorizeApi]
		public int Post([FromBody]IEnumerable<Answer> answers)
		{
			return Vote(answers, UserId);
		}

		[HttpGet]
		[Route("total")]
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
			throw new VotingSystemException(Errors.InvalidCaptcha);
		}

		#endregion

		#region Private methods

		private int Vote(IEnumerable<Answer> answers, int? userId)
		{
			int votingId = _votingService.GetVotingByQuestionId(answers.First().QuestionId).Id;
			if (userId != null && _answerService.IsVotingAnswered(votingId, userId.Value))
			{
				throw new VotingSystemException(Errors.AlreadyAnsweredVoting);
			}
			if (!_votingService.IsVotingClosed(votingId))
			{
				_answerService.AddAnswer(answers, userId);
				List<Answer> answersToVoting = _answerService.GetByVotingId(votingId, new Filter(1, int.MaxValue));
				return answersToVoting.Count(a => a.QuestionId == answersToVoting.First().QuestionId);
			}
			throw new VotingSystemException(Errors.VotingClosedOrBlocked);
		}

		#endregion
	}
}