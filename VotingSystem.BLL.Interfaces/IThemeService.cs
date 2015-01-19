using System.Collections.Generic;
using VotingSystem.Common;
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

		List<Theme> GetThemesByUserId(string query, int userId, FilterExtended<Theme> filter);

		List<Theme> GetAllThemes(string query, FilterExtended<Theme> filterExtended);

		List<Theme> GetAllActiveThemes(string query, FilterExtended<Theme> filter);

		int GetNumberOfThemesByThemeName(string partOfThemeName);

		int GetNumberOfActiveThemesByThemeName(string partOfThemeName);

		int GetNumberOfUserThemes(int userId, string partOfThemeName);

		void UpdateStatus(int themeId, StatusType status);
	}
}