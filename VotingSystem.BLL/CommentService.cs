using System.Collections.Generic;
using System.Linq;
using VotingSystem.BLL.Interfaces;
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

		public void Insert(Comment comment)
		{
			UnitOfWork.CommentRepository.Insert(comment);
			UnitOfWork.Save();
		}

		public void Delete(int commentId)
		{
			UnitOfWork.CommentRepository.Delete(UnitOfWork.CommentRepository.GetById(commentId));
			UnitOfWork.Save();
		}

		public List<Comment> GetByUserId(int userId, int page = 1, int pageSize = 10)
		{
			return UnitOfWork.CommentRepository.Query()
				.Filter(c => c.UserId == userId)
				.OrderBy(co => co.OrderByDescending(com => com.CreateDate))
				.GetPage(page, pageSize).ToList();
		}

		public List<Comment> GetByThemeId(int themeId, int page = 1, int pageSize = 10)
		{
			return UnitOfWork.CommentRepository.Query()
				.Filter(c => c.ThemeId == themeId)
				.OrderBy(co => co.OrderByDescending(com => com.CreateDate))
				.GetPage(page, pageSize).ToList();
		}

		public Comment GetByCommentId(int commentId)
		{
			return UnitOfWork.CommentRepository.GetById(commentId);
		}

		public int GetMyTotal(int userId)
		{
			return UnitOfWork.CommentRepository.GetTotal(c => c.UserId == userId);
		}
	}
}
