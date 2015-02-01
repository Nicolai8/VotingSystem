using System.Collections.Generic;
using System.Linq;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common.Filters;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class CommentService : BaseService, ICommentService
	{
		public CommentService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void InsertComment(Comment comment)
		{
			UnitOfWork.CommentRepository.Insert(comment);
			UnitOfWork.Save();
		}

		public void DeleteComment(int commentId)
		{
			UnitOfWork.CommentRepository.Delete(UnitOfWork.CommentRepository.GetById(commentId));
			UnitOfWork.Save();
		}

		public List<Comment> GetCommentByUserId(int userId, Filter filter)
		{
			return UnitOfWork.CommentRepository.Query()
				.Filter(c => c.UserId == userId)
				.OrderBy(co => co.OrderByDescending(com => com.CreateDate))
				.GetPage(filter.Page, filter.PageSize).ToList();
		}

		public List<Comment> GetByThemeId(int themeId, int page = 1, int pageSize = 10)
		{
			return UnitOfWork.CommentRepository.Query()
				.Filter(c => c.ThemeId == themeId)
				.OrderBy(co => co.OrderByDescending(com => com.CreateDate))
				.GetPage(page, pageSize).ToList();
		}

		public Comment GetCommentById(int commentId)
		{
			return UnitOfWork.CommentRepository.GetById(commentId);
		}

		public int GetNumberOfUserComments(int userId)
		{
			return UnitOfWork.CommentRepository.GetTotal(c => c.UserId == userId);
		}
	}
}
