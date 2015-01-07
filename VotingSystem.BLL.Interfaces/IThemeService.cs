using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IThemeService
	{
		//REVIEW: Need to find one way in service methods naming. There method called Insert in IUserService we have CreateUser method.
		void Insert(Theme theme);
		//REVIEW: Need to find one way in service methods naming. There method called Delete in IUserService we have DeleteUser method.
		void Delete(int themeId);
		bool IsClosed(int themeId);
		//REVIEW: Better name is GetThemeById
		Theme GetByThemeId(int themeId);
		//REVIEW: Better name is GetThemeByQuestionId
		Theme GetByQuestionId(int questionId);
		List<Theme> GetByUserId(string query, int userId, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10);
		List<Theme> GetAll(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
	int page = 1, int pageSize = 10);
		List<Theme> GetAllActive(string query, Func<IQueryable<Theme>, IOrderedQueryable<Theme>> orderBy = null,
			int page = 1, int pageSize = 10);
		//REVIEW: Need to rename. Because not clear what this method does. Also possibly needo be added summary comment.
		int GetTotal(string query);
		//REVIEW: Need to rename. Because not clear what this method does. Also possibly needo be added summary comment.
		int GetTotalActive(string query);
		//REVIEW: Need to rename. Because not clear what this method does. Also possibly needo be added summary comment.
		int GetTotal(int userId, string query);
		//REVIEW: Need to rename. Because not clear what this method does. Also possibly needo be added summary comment.
		//REVIEW: If votingId == themeId then better to use in parameter name last one. votingId is not clear.
		void UpdateStatus(int votingId, StatusType status);
	}
}