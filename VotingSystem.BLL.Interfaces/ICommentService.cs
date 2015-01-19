using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface ICommentService
	{
		void InsertComment(Comment comment);

		void DeleteComment(int commentId);

		//REVIEW: Could Filter functionality be applied there?
		List<Comment> GetCommentByUserId(int userId, int page = 1, int pageSize = 10);

		Comment GetCommentById(int commentId);

		int GetNumberOfUserComments(int userId);
	}
}