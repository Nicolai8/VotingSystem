using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AutoMapper;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Structures;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Helpers
{
	public static class ConvertHelper
	{
		public static VotingModel ToVotingModel(this Theme theme, string userName)
		{
			//REVIEW: should be constant or better separate formatter helper
			Mapper.CreateMap<Theme, VotingModel>()
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.AnswersCount, mo => mo.MapFrom(s => s.Questions.First().Answers.Count))
				.ForMember(d => d.CommentsCount, mo => mo.MapFrom(s => s.Comments.Count))
				.ForMember(d => d.StartDate, mo => mo.MapFrom(s => s.StartDate.ToString("dd-MM-yy")))
				.ForMember(d => d.EndDate, mo => mo.MapFrom(s => s.FinishTime.ToString("dd-MM-yy")))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate.ToString("f")))
				.ForMember(d => d.Status,
					mo => mo.MapFrom(s =>
						s.StartDate.Date <= DateTime.Today && s.FinishTime.Date >= DateTime.Today
							? s.Status
							: StatusType.Closed))
				.AfterMap((t, vm) => vm.CreatedBy = userName);

			return Mapper.Map<Theme, VotingModel>(theme);
		}

		public static VotingPageModel ToVotingPageModel(this Theme theme, bool isAnswered, string userName)
		{
			Mapper.CreateMap<Theme, VotingPageModel>()
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.TotalVotes, mo => mo.MapFrom(s => s.Questions.First().Answers.Count))
				.ForMember(d => d.Comments, mo => mo.Ignore())
				.ForMember(d => d.StartDate, mo => mo.MapFrom(s => s.StartDate.ToString("dd-MM-yy")))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate.ToString("f")))
				.ForMember(d => d.Status, mo => mo.MapFrom(s => s.StartDate.Date <= DateTime.Today && s.FinishTime.Date >= DateTime.Today ? s.Status : StatusType.Closed))
				.ForMember(d => d.CreatedBy, mo => mo.MapFrom(s => s.User != null ? s.User.UserName : string.Empty))
				.ForMember(d => d.Questions, mo => mo.MapFrom(s => s.Questions.Select(q => q.ToQuestionModel())))
				.AfterMap((s, d) =>
					{
						d.IsAnswered = isAnswered;
						d.Comments = s.Comments.OrderByDescending(c => c.CreateDate).ToList()
							.Select(comment => comment.ToCommentModel(userName)).ToList();
						d.TimeLeft = s.FinishTime.Date >= DateTime.Today && s.StartDate.Date <= DateTime.Today
							? String.Format(Resources.Resource.TimeLeftFormatString, s.FinishTime.Subtract(DateTime.Now))
							: "Voting closed";
					});

			return Mapper.Map<Theme, VotingPageModel>(theme);
		}

		public static UserModel ToUserModel(this MembershipUser user, UserProfile userProfile = null)
		{
			Mapper.CreateMap<MembershipUser, UserModel>()
				.ForMember(d => d.UserId, mo => mo.MapFrom(s => (int)s.ProviderUserKey))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreationDate.ToString("dd-MM-yy")))
				.ForMember(d => d.Roles, mo => mo.MapFrom(s => Roles.GetRolesForUser(s.UserName)))
				.ForMember(d => d.IsBlocked, mo => mo.MapFrom(s => s.IsLockedOut))
				.AfterMap((s, d) =>
					{
						if (userProfile != null)
						{
							d.PictureUrl = String.IsNullOrEmpty(userProfile.PictureUrl)
								? GlobalVariables.DefaultImagePath
								: userProfile.PictureUrl;
							d.Privacy = userProfile.Privacy;
						}
					});

			return Mapper.Map<MembershipUser, UserModel>(user);
		}

		public static CommentModel ToCommentModel(this Comment comment, string currentUserName)
		{
			Mapper.CreateMap<Comment, CommentModel>()
				.ForMember(d => d.CommentId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.ThemeId))
				.ForMember(d => d.VotingName, mo => mo.MapFrom(s => s.Theme.VotingName))
				.ForMember(d => d.CreatedBy, mo => mo.MapFrom(s => s.User.UserName))
				.ForMember(d => d.PictureUrl, mo => mo.MapFrom(s => s.User.UserProfile.PictureUrl ?? GlobalVariables.DefaultImagePath))
				.ForMember(d => d.Own, mo => mo.Ignore())
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate.ToString("dd-MM-yy hh:mm")))
				.AfterMap((s, d) => d.Own = s.User.UserName.Equals(currentUserName));

			return Mapper.Map<Comment, CommentModel>(comment);
		}

		public static AnswerModel ToAnswerModel(this Answer answer, string pictureUrl = "")
		{
			Mapper.CreateMap<Answer, AnswerModel>()
				.ForMember(d => d.AnswerText, mo => mo.MapFrom(s => s.FixedAnswer == null ? s.AnswerText : s.FixedAnswer.AnswerText))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate.ToString("dd-MM-yy")))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Question.ThemeId))
				.ForMember(d => d.VotingName, mo => mo.MapFrom(s => s.Question.Theme.VotingName))
				.AfterMap((s, d) => d.PictureUrl = pictureUrl);

			return Mapper.Map<Answer, AnswerModel>(answer);
		}

		public static QuestionModel ToQuestionModel(this Question question)
		{
			Mapper.CreateMap<Question, QuestionModel>()
				.ForMember(d => d.QuestionId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.ThemeId))
				.ForMember(d => d.FixedAnswers,
					mo => mo.MapFrom(s => s.FixedAnswers.Select(fa => fa.ToFixedAnswerModel()).ToList()))
				.ForMember(d => d.Answers, mo => mo.Ignore());

			return Mapper.Map<Question, QuestionModel>(question);
		}

		public static QuestionModel ToQuestionModel(this Question question, int userId)
		{
			Mapper.CreateMap<Question, QuestionModel>()
				.ForMember(d => d.QuestionId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.ThemeId))
				.ForMember(d => d.FixedAnswers, mo => mo.Ignore())
				.AfterMap((s, d) =>
					{
						Answer userAnswer = s.Answers.FirstOrDefault(qa => qa.UserId == userId);
						if (s.Type == QuestionType.OpenQuestion)
						{
							d.Answers = s.Answers.GroupBy(a => a.AnswerText).Select(g => new AnswerModel
							{
								AnswerText = g.Key,
								Count = g.Count(),
								IsUserAnswer = userAnswer != null && userAnswer.AnswerText.Equals(g.Key)
							});
						}
						else
						{
							d.Answers = s.FixedAnswers.Select(fa => new AnswerModel
							{
								AnswerText = fa.AnswerText,
								Count = s.Answers.Count(qa => qa.FixedAnswerId.Equals(fa.Id)),
								IsUserAnswer = userAnswer != null && fa.Id.Equals(userAnswer.FixedAnswerId)
							});
						}
					});


			return Mapper.Map<Question, QuestionModel>(question);
		}

		public static FixedAnswerModel ToFixedAnswerModel(this FixedAnswer answer)
		{
			Mapper.CreateMap<FixedAnswer, FixedAnswerModel>();
			return Mapper.Map<FixedAnswer, FixedAnswerModel>(answer);
		}
	}
}