using System.Collections.Generic;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface ICommentService
	{
		void InsertComment(Comment comment);

		void DeleteComment(int commentId);

		List<Comment> GetCommentByUserId(int userId, Filter filter);

		Comment GetCommentById(int commentId);

		int GetNumberOfUserComments(int userId);
	}
}