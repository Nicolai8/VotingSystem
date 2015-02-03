using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.Common.Filters;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;
using VotingSystem.Web.Resources;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/voting")]
	public class VotingApiController : BaseApiController
	{
		private readonly IVotingService _votingService;
		private readonly IAnswerService _answerService;

		public VotingApiController(IVotingService votingService, IAnswerService answerService)
		{
			_votingService = votingService;
			_answerService = answerService;
		}

		[Route("{id:int}")]
		public VotingPageModel Get(int id)
		{
			Voting voting = _votingService.GetVotingById(id);
			if (voting != null)
			{
				string userName = string.Empty;
				bool isAnswered = false;
				if (User.Identity.IsAuthenticated)
				{
					userName = User.Identity.Name;
					isAnswered = _answerService.IsVotingAnswered(id, UserId);
				}

				return voting.ToVotingPageModel(isAnswered, userName);
			}

			throw new VotingSystemException(Errors.VotingNotFound);
		}

		[Route("{pageType}/{page:int}/{query?}")]
		public IEnumerable<VotingModel> Get(PageType pageType, int page = 1, int size = 10, string query = null)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				query = string.Empty;
			}
			List<Voting> votings;
			Filter<Voting> filterExtended = new Filter<Voting>(null, page, size);

			switch (pageType)
			{
				case PageType.UserVotings:
					votings = _votingService.GetVotingsByUserId(query, UserId, filterExtended);
					break;
				case PageType.AdminVotings:
					if (!User.IsInRole(RoleType.Admin.ToString()) && !User.IsInRole(RoleType.Moderator.ToString()))
					{
						throw new AuthenticationException();
					}
					votings = _votingService.GetAllVotings(query, filterExtended);
					break;
				default:
					votings = _votingService.GetAllActiveVotings(query, filterExtended);
					break;
			}

			List<VotingModel> model = votings.Select(t =>
				{
					MembershipUser membershipUser = Membership.GetUser(t.UserId ?? -1);
					return t.ToVotingModel(membershipUser != null ? membershipUser.UserName : string.Empty);
				}).ToList();
			return model;
		}

		[HttpGet]
		[Route("totalActive")]
		public int GetTotalActiveVotings(string query = null)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				query = string.Empty;
			}
			return _votingService.GetNumberOfActiveVotingsByVotingName(query);
		}

		[HttpGet]
		[Route("totalUser")]
		[CustomAuthorizeApi]
		public int GetTotalUserVotings(string query = null)
		{
			if (String.IsNullOrEmpty(query))
			{
				query = string.Empty;
			}
			return _votingService.GetNumberOfUserVotings(UserId, query);
		}

		[HttpGet]
		[Route("totalAdmin")]
		[CustomAuthorizeApi(Roles = new[] { RoleType.Admin, RoleType.Moderator })]
		public int GetTotalAdminVotings(string query = null)
		{
			if (String.IsNullOrEmpty(query))
			{
				query = string.Empty;
			}
			return _votingService.GetNumberOfVotingsByVotingName(query);
		}

		[HttpPost]
		[Route("")]
		[CustomAuthorizeApi]
		public void Post([FromBody]Voting voting)
		{
			if (ModelState.IsValid)
			{
				voting.UserId = UserId;
				voting.CreateDate = DateTime.Now;
				voting.Status = VotingStatusType.NotApproved;
				_votingService.InsertVoting(voting);

				return;
			}
			throw new VotingSystemException(Errors.CreateNewVotingFailed);
		}

		[Route("{id:int}")]
		[OwnsApi(OwnsParameter = OwnsType.Voting)]
		public VotingModel Put(int id, [FromBody]VotingModel voting)
		{
			_votingService.UpdateVotingStatus(id, voting.Status.Value);
			return voting;
		}

		[Route("{id:int}")]
		[OwnsApi(OwnsParameter = OwnsType.Voting)]
		public void Delete(int id)
		{
			_votingService.DeleteVoting(id);
		}
	}
}