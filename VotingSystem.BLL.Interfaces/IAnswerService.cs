using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IAnswerService
	{
		//REVIEW: Possibly need to make this method private. Because nobody use it
		void Insert(Answer answer);
		//REVIEW: Possibly need to make this method private. Because nobody use it
		void Delete(int answerId);
		List<Answer> GetByUserId(int userId, int page = 1, int pageSize = 10);
		List<Answer> GetByThemeId(int themeId, int page = 1, int pageSize = 10);
		bool IsAnswered(int themeId, int userId);
		//REVIEW: Possibly need to make this method private. Because nobody use it
		//REVIEW: Better if methd name will contains a verb. Answer is not clear. Better: 
		void Answer(Answer answer, int? userId = null);
		void Answer(IEnumerable<Answer> answers, int? userId = null);
		int GetMyTotal(int userId);
	}
}
