﻿using System;
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
	public class ThemeService : BaseService, IThemeService
	{
		public ThemeService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void InsertTheme(Theme theme)
		{
			theme.StartDate = theme.StartDate.Date;
			theme.FinishTime = theme.FinishTime.Date;
			UnitOfWork.ThemeRepository.Insert(theme);
			UnitOfWork.Save();
		}

		public void DeleteTheme(int themeId)
		{
			UnitOfWork.ThemeRepository.Delete(themeId);
			UnitOfWork.Save();
		}

		public bool IsThemeClosed(int themeId)
		{
			Theme theme = UnitOfWork.ThemeRepository.GetById(themeId);
			return theme.Status == VotingStatusType.Closed || theme.Status == VotingStatusType.Blocked
				|| theme.StartDate.Date > DateTime.Today || theme.FinishTime.Date < DateTime.Today;
		}

		public Theme GetThemeById(int themeId)
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

		public Theme GetThemeByQuestionId(int questionId)
		{
			return UnitOfWork.ThemeRepository.Query()
				.Filter(t => t.Questions.Any(q => q.Id == questionId))
				.Get().FirstOrDefault();
		}

		public List<Theme> GetThemesByUserId(string query, int userId, Filter<Theme> filterExtended)
		{
			return GetThemes(filterExtended, t => t.UserId == userId && t.VotingName.Contains(query)).ToList();
		}

		public List<Theme> GetAllThemes(string query, Filter<Theme> filterExtended)
		{
			return GetThemes(filterExtended, t => t.VotingName.Contains(query));
		}

		public List<Theme> GetAllActiveThemes(string query, Filter<Theme> filterExtended)
		{
			return GetThemes(filterExtended, t => (t.Status == VotingStatusType.Active || t.Status == VotingStatusType.NotApproved) &&
				(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
				&& t.VotingName.Contains(query));
		}

		public int GetNumberOfThemesByThemeName(string partOfThemeName)
		{
			return UnitOfWork.ThemeRepository.GetTotal(t => t.VotingName.Contains(partOfThemeName));
		}

		public int GetNumberOfActiveThemesByThemeName(string partOfThemeName)
		{
			return UnitOfWork.ThemeRepository.GetTotal(
				t => (t.Status == VotingStatusType.Active || t.Status == VotingStatusType.NotApproved) &&
					(t.FinishTime >= DateTime.Today && t.StartDate <= DateTime.Today)
					&& t.VotingName.Contains(partOfThemeName));
		}

		public int GetNumberOfUserThemes(int userId, string partOfThemeName)
		{
			return UnitOfWork.ThemeRepository.GetTotal(t => t.UserId == userId && t.VotingName.Contains(partOfThemeName));
		}

		public void UpdateStatus(int themeId, VotingStatusType status)
		{
			Theme theme = GetThemeById(themeId);
			if (theme != null)
			{
				theme.Status = status;
				theme.FinishTime = DateTime.Now;
				UnitOfWork.ThemeRepository.Update(theme);
				UnitOfWork.Save();
			}
		}

		private List<Theme> GetThemes(Filter<Theme> filterExtended, Expression<Func<Theme, bool>> expression = null)
		{
			if (filterExtended.OrderBy == null)
			{
				filterExtended.OrderBy = t => t.OrderByDescending(theme => theme.CreateDate);
			}
			return UnitOfWork.ThemeRepository.Query()
				.Filter(expression)
				.OrderBy(filterExtended.OrderBy)
				.Include(t => t.Comments)
				.Include(t => t.Questions)
				.Include(t => t.Questions.Select(q => q.Answers))
				.GetPage(filterExtended.Page, filterExtended.PageSize).ToList();
		}
	}
}
