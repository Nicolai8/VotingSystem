using System.Collections.Generic;
using VotingSystem.Common.Filters;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IAnswerService
	{
		List<Answer> GetByUserId(int userId, Filter filter);

		List<Answer> GetByVotingId(int votingId, Filter filter);

		bool IsVotingAnswered(int votingId, int userId);

		void AddAnswer(Answer answer, int? userId = null);

		void AddAnswer(IEnumerable<Answer> answers, int? userId = null);

		int GetNumberOfUserAnswers(int userId);
	}
}
