using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IAnswerService
	{
		//REVIEW: Could Filter functionality be applied there?
		List<Answer> GetByUserId(int userId, int page = 1, int pageSize = 10);

		//REVIEW: Could Filter functionality be applied there?
		List<Answer> GetByThemeId(int themeId, int page = 1, int pageSize = 10);

		bool IsThemeAnswered(int themeId, int userId);

		void AddAnswer(Answer answer, int? userId = null);

		void AddAnswer(IEnumerable<Answer> answers, int? userId = null);

		int GetNumberOfUserAnswers(int userId);
	}
}
