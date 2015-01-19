using System.Collections.Generic;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Structures;

namespace VotingSystem.BLL.Interfaces
{
	public interface IThemeService
	{
		void InsertTheme(Theme theme);

		void DeleteTheme(int themeId);

		//REVIEW: Better name is IsThemeClosed
		bool IsClosed(int themeId);

		Theme GetThemeById(int themeId);

		Theme GetThemeByQuestionId(int questionId);

		List<Theme> GetThemesByUserId(string query, int userId, Filter<Theme> filter);

		List<Theme> GetAllThemes(string query, Filter<Theme> filter);

		List<Theme> GetAllActiveThemes(string query, Filter<Theme> filter);

		int GetNumberOfThemesByThemeName(string partOfThemeName);

		int GetNumberOfActiveThemesByThemeName(string partOfThemeName);

		int GetNumberOfUserThemes(int userId, string partOfThemeName);

		void UpdateStatus(int themeId, StatusType status);
	}
}