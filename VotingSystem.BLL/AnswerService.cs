﻿using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common.Filters;
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

		public List<Answer> GetByUserId(int userId, Filter filter)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(a => a.UserId == userId)
				.OrderBy(an => an.OrderByDescending(ans => ans.CreateDate))
				.Include(a => a.Question)
				.Include(a => a.Question.Voting)
				.Include(a => a.FixedAnswer)
				.GetPage(filter.Page, filter.PageSize).ToList();
		}

		public List<Answer> GetByVotingId(int votingId, Filter filter)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(a => a.Question.VotingId == votingId)
				.OrderBy(an => an.OrderByDescending(ans => ans.CreateDate))
				.GetPage(filter.Page, filter.PageSize).ToList();
		}

		public bool IsVotingAnswered(int votingId, int userId)
		{
			return UnitOfWork.AnswerRepository.Query()
				.Filter(answer => answer.Question.VotingId == votingId && answer.UserId == userId)
				.Get().Any();
		}

		public void AddAnswer(Answer answer, int? userId = null)
		{
			answer.UserId = userId;
			answer.CreateDate = DateTime.Now;
			Insert(answer);
		}

		public void AddAnswer(IEnumerable<Answer> answers, int? userId = null)
		{
			foreach (Answer answer in answers)
			{
				AddAnswer(answer, userId);
			}
		}

		public int GetNumberOfUserAnswers(int userId)
		{
			return UnitOfWork.AnswerRepository.GetTotal(a => a.UserId == userId);
		}

		private void Insert(Answer answer)
		{
			UnitOfWork.AnswerRepository.Insert(answer);
			UnitOfWork.Save();
		}
	}
}
