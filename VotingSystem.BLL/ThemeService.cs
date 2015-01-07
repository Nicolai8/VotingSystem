using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class ThemeService : BaseService, IThemeService
	{
		public ThemeService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Insert(Theme theme)
		{
			theme.StartDate = theme.StartDate.Date;
			theme.FinishTime = theme.FinishTime.Date;
			UnitOfWork.ThemeRepository.Insert(theme);
			UnitOfWork.Save();
		}

		public void Delete(int themeId)
		{
			UnitOfWork.ThemeRepository.Delete(themeId);
			UnitOfWork.Save();
		}

		public bool IsClosed(int themeId)
		{
			Theme theme = UnitOfWork.ThemeRepository.GetById(themeId);
			return theme.Status == StatusType.Closed || theme.Status == StatusType.Blocked
				|| theme.StartDate.Date > DateTime.Today || theme.FinishTime.Date < DateTime.Today;
		}

		public Theme GetByThemeId(int themeId)
		{
			return UnitOfWork.ThemeRepository.Query()
				.Filter(theme => theme.Id == themeId)
				.Include(t => t.User)
				.Include(t => t.Questions)
				.Include(t => t.Questions.Select(q => q.Answers))
				.Include(t => t.Questions.Select(q => q.FixedAnswers))
				.Include(t => t.Comments)
				.Include(t => t.Comments.Select(c => c.User))
				.Include(t => t.Comments.Select(c => c.User.UserProfile))
				.Get().SingleOrDefault();
		}

		public Theme GetByQuestionId(int questionId)
		{
			return UnitOfWork.ThemeRepository.Query()
				.Filter(t => t.Questions.Any(q => q.Id == questionId))
				.Get().FirstOrDefault();
		}

		public List<Theme> GetByUserId(string query, int userId, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10)
		{
			return Get(t => t.UserId == userId && t.VotingName.Contains(query), orderBy, page, pageSize).ToList();
		}

		//REVIEW: Possible need to create generic Filter class which will contain Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null, int page = 1, int pageSize = 10
		public List<Theme> GetAll(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null, int page = 1, int pageSize = 10)
		{
			return Get(t => t.VotingName.Contains(query), orderBy, page, pageSize);
		}

		//REVIEW: Possible need to create generic Filter class which will contain Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null, int page = 1, int pageSize = 10
		public List<Theme> GetAllActive(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null, int page = 1, int pageSize = 10)
		{
			return Get(t => (t.Status == StatusType.Active || t.Status == StatusType.NotApproved) &&
				(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
				&& t.VotingName.Contains(query), orderBy, page, pageSize);
		}

		//REVIEW: Possible need to create generic Filter class which will contain Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null, int page = 1, int pageSize = 10
		//REVIEW: Move to the end of the class. Pls, check : http://stackoverflow.com/a/150540/710014
		private List<Theme> Get(Expression<Func<Theme, bool>> filter = null, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10)
		{
			if (orderBy == null)
			{
				orderBy = t => t.OrderByDescending(theme => theme.CreateDate);
			}
			return UnitOfWork.ThemeRepository.Query()
				.Filter(filter)
				.OrderBy(orderBy)
				.Include(t => t.Comments)
				.Include(t => t.Questions)
				.Include(t => t.Questions.Select(q=>q.Answers))
				.GetPage(page, pageSize).ToList();
		}

		public int GetTotal(string query)
		{
			return UnitOfWork.ThemeRepository.GetTotal(t => t.VotingName.Contains(query));
		}

		public int GetTotalActive(string query)
		{
			return UnitOfWork.ThemeRepository.GetTotal(
				t => (t.Status == StatusType.Active || t.Status == StatusType.NotApproved) &&
					(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
					&& t.VotingName.Contains(query));
		}

		public int GetTotal(int userId, string query)
		{
			return UnitOfWork.ThemeRepository.GetTotal(t => t.UserId == userId && t.VotingName.Contains(query));
		}

		public void UpdateStatus(int votingId, StatusType status)
		{
			Theme theme = GetByThemeId(votingId);
			if (theme != null)
			{
				theme.Status = status;
				theme.FinishTime = DateTime.Now;
				UnitOfWork.ThemeRepository.Update(theme);
				UnitOfWork.Save();
			}
		}
	}
}
