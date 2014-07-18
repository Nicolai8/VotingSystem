﻿using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class AnswerService : BaseService, IAnswerService
	{
		public AnswerService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Insert(Answer answer)
		{
			UnitOfWork.AnswerRepository.Insert(answer);
			UnitOfWork.Save();
		}

		public void Delete(int answerId)
		{
			UnitOfWork.AnswerRepository.Delete(answerId);
			UnitOfWork.Save();
		}

		public List<Answer> GetByUserId(int userId, int page = 1, int pageSize = 10)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(a => a.UserId == userId)
				.OrderBy(an => an.OrderByDescending(ans => ans.CreateDate))
				.Include(a => a.Question)
				.Include(a => a.Question.Theme)
				.Include(a => a.FixedAnswer)
				.GetPage(page, pageSize).ToList();
		}

		public List<Answer> GetByThemeId(int themeId, int page = 1, int pageSize = 10)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(a => a.Question.ThemeId == themeId)
				.OrderBy(an => an.OrderByDescending(ans => ans.CreateDate))
				.GetPage(page, pageSize).ToList();
		}

		public bool IsAnswered(int themeId, int userId)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(answer => answer.Question.ThemeId == themeId && answer.UserId == userId)
				.Get().Any();
		}

		public void Answer(Answer answer, int? userId = null)
		{
			answer.UserId = userId;
			answer.CreateDate = DateTime.Now;
			Insert(answer);
		}

		public void Answer(IEnumerable<Answer> answers, int? userId = null)
		{
			foreach (Answer answer in answers)
			{
				Answer(answer, userId);
			}
		}

		public int GetMyTotal(int userId)
		{
			return UnitOfWork.AnswerRepository.GetTotal(a => a.UserId == userId);
		}
	}
}
