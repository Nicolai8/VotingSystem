using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface ICommentService
	{
		void Insert(Comment comment);
		void Delete(int commentId);
		//REVIEW: Better name is GetByUserId
		List<Comment> GetByUserId(int userId, int page = 1, int pageSize = 10);
		//REVIEW: Nobody use it. Could be removed
		List<Comment> GetByThemeId(int themeId, int page = 1, int pageSize = 10);
		//REVIEW: Better name is GetById
		Comment GetByCommentId(int commentId);
		//REVIEW: Better name is GetNumberOfUserComments or so. Using 'My' is not a good practise.
		int GetMyTotal(int userId);
	}
}