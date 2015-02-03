using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.Common.Filters;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;

namespace VotingSystem.BLL
{
	public class VotingService : BaseService, IVotingService
	{
		public VotingService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void InsertVoting(Voting voting)
		{
			voting.StartDate = voting.StartDate.Date;
			voting.FinishTime = voting.FinishTime.Date;
			UnitOfWork.VotingRepository.Insert(voting);
			UnitOfWork.Save();
		}

		public void DeleteVoting(int votingId)
		{
			UnitOfWork.VotingRepository.Delete(votingId);
			UnitOfWork.Save();
		}

		public bool IsVotingClosed(int votingId)
		{
			Voting voting = UnitOfWork.VotingRepository.GetById(votingId);
			return voting.Status == VotingStatusType.Closed || voting.Status == VotingStatusType.Blocked
				|| voting.StartDate.Date > DateTime.Today || voting.FinishTime.Date < DateTime.Today;
		}

		public Voting GetVotingById(int votingId)
		{
			return UnitOfWork.VotingRepository.Query()
				.Filter(voting => voting.Id == votingId)
				.Include(t => t.User)
				.Include(t => t.Questions)
				.Include(t => t.Questions.Select(q => q.Answers))
				.Include(t => t.Questions.Select(q => q.FixedAnswers))
				.Include(t => t.Comments)
				.Include(t => t.Comments.Select(c => c.User))
				.Include(t => t.Comments.Select(c => c.User.UserProfile))
				.Get().SingleOrDefault();
		}

		public Voting GetVotingByQuestionId(int questionId)
		{
			return UnitOfWork.VotingRepository.Query()
				.Filter(t => t.Questions.Any(q => q.Id == questionId))
				.Get().FirstOrDefault();
		}

		public List<Voting> GetVotingsByUserId(string query, int userId, Filter<Voting> filterExtended)
		{
			return GetVotings(filterExtended, t => t.UserId == userId && t.VotingName.Contains(query)).ToList();
		}

		public List<Voting> GetAllVotings(string query, Filter<Voting> filterExtended)
		{
			return GetVotings(filterExtended, t => t.VotingName.Contains(query));
		}

		public List<Voting> GetAllActiveVotings(string query, Filter<Voting> filterExtended)
		{
			return GetVotings(filterExtended, t => (t.Status == VotingStatusType.Active || t.Status == VotingStatusType.NotApproved) &&
				(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
				&& t.VotingName.Contains(query));
		}

		public int GetNumberOfVotingsByVotingName(string partOfVotingName)
		{
			return UnitOfWork.VotingRepository.GetTotal(t => t.VotingName.Contains(partOfVotingName));
		}

		public int GetNumberOfActiveVotingsByVotingName(string partOfVotingName)
		{
			return UnitOfWork.VotingRepository.GetTotal(
				t => (t.Status == VotingStatusType.Active || t.Status == VotingStatusType.NotApproved) &&
					(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
					&& t.VotingName.Contains(partOfVotingName));
		}

		public int GetNumberOfUserVotings(int userId, string partOfVotingName)
		{
			return UnitOfWork.VotingRepository.GetTotal(t => t.UserId == userId && t.VotingName.Contains(partOfVotingName));
		}

		public void UpdateVotingStatus(int votingId, VotingStatusType status)
		{
			Voting voting = GetVotingById(votingId);
			if (voting != null)
			{
				voting.Status = status;
				voting.FinishTime = DateTime.Now;
				UnitOfWork.VotingRepository.Update(voting);
				UnitOfWork.Save();
			}
		}

		private List<Voting> GetVotings(Filter<Voting> filterExtended, Expression<Func<Voting, bool>> expression = null)
		{
			if (filterExtended.OrderBy == null)
			{
				filterExtended.OrderBy = t => t.OrderByDescending(voting => voting.CreateDate);
			}
			return UnitOfWork.VotingRepository.Query()
				.Filter(expression)
				.OrderBy(filterExtended.OrderBy)
				.Include(t => t.Comments)
				.Include(t => t.Questions)
				.Include(t => t.Questions.Select(q => q.Answers))
				.GetPage(filterExtended.Page, filterExtended.PageSize).ToList();
		}
	}
}
