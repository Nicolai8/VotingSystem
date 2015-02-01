using System.Collections.Generic;
using VotingSystem.Common.Filters;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;

namespace VotingSystem.BLL.Interfaces
{
	public interface IThemeService
	{
		void InsertTheme(Theme theme);

		void DeleteTheme(int themeId);

		bool IsThemeClosed(int themeId);

		Theme GetThemeById(int themeId);

		Theme GetThemeByQuestionId(int questionId);

		List<Theme> GetThemesByUserId(string query, int userId, Filter<Theme> filter);

		List<Theme> GetAllThemes(string query, Filter<Theme> filterExtended);

		List<Theme> GetAllActiveThemes(string query, Filter<Theme> filter);

		int GetNumberOfThemesByThemeName(string partOfThemeName);

		int GetNumberOfActiveThemesByThemeName(string partOfThemeName);

		int GetNumberOfUserThemes(int userId, string partOfThemeName);

		// REVIEW: Update Status of what or whom? Name of method
		void UpdateStatus(int themeId, VotingStatusType status);
	}
}