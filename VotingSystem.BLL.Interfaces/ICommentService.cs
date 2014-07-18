using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface ICommentService
	{
		void Insert(Comment comment);
		void Delete(int commentId);
		List<Comment> GetByUserId(int userId, int page = 1, int pageSize = 10);
		List<Comment> GetByThemeId(int themeId, int page = 1, int pageSize = 10);
		Comment GetByCommentId(int commentId);
		int GetMyTotal(int userId);
	}
}