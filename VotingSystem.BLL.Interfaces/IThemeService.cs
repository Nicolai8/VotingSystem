using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IThemeService
	{
		void Insert(Theme theme);
		void Delete(int themeId);
		bool IsClosed(int themeId);
		Theme GetByThemeId(int themeId);
		Theme GetByQuestionId(int questionId);
		List<Theme> GetByUserId(string query, int userId, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10);
		List<Theme> GetAll(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
	int page = 1, int pageSize = 10);
		List<Theme> GetAllActive(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10);
		int GetTotal(string query);
		int GetTotalActive(string query);
		int GetTotal(int userId, string query);
		void UpdateStatus(int votingId, StatusType status);
	}
}