using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IAnswerService
	{
		void Insert(Answer answer);
		void Delete(int answerId);
		List<Answer> GetByUserId(int userId, int page = 1, int pageSize = 10);
		List<Answer> GetByThemeId(int themeId, int page = 1, int pageSize = 10);
		bool IsAnswered(int themeId, int userId);
		void Answer(Answer answer, int? userId = null);
		void Answer(IEnumerable<Answer> answers, int? userId = null);
		int GetMyTotal(int userId);
	}
}
