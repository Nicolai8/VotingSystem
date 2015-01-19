using System.Collections.Generic;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IAnswerService
	{
		List<Answer> GetByUserId(int userId, Filter filter);

		List<Answer> GetByThemeId(int themeId, Filter filter);

		bool IsThemeAnswered(int themeId, int userId);

		void AddAnswer(Answer answer, int? userId = null);

		void AddAnswer(IEnumerable<Answer> answers, int? userId = null);

		int GetNumberOfUserAnswers(int userId);
	}
}
