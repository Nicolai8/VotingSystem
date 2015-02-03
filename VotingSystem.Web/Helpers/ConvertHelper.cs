using System;
using System.Linq;
using System.Web.Security;
using AutoMapper;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Helpers
{
	public static class ConvertHelper
	{
		public static VotingModel ToVotingModel(this Voting voting, string userName)
		{
			Mapper.CreateMap<Voting, VotingModel>()
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.AnswersCount, mo => mo.MapFrom(s => s.Questions.First().Answers.Count))
				.ForMember(d => d.CommentsCount, mo => mo.MapFrom(s => s.Comments.Count))
				.ForMember(d => d.StartDate, mo => mo.MapFrom(s => s.StartDate))
				.ForMember(d => d.EndDate, mo => mo.MapFrom(s => s.FinishTime))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate))
				.ForMember(d => d.Status,
					mo => mo.MapFrom(s =>
						s.StartDate.Date <= DateTime.Today && s.FinishTime.Date >= DateTime.Today
							? s.Status
							: VotingStatusType.Closed))
				.AfterMap((t, vm) => vm.CreatedBy = userName);

			return Mapper.Map<Voting, VotingModel>(voting);
		}

		public static VotingPageModel ToVotingPageModel(this Voting voting, bool isAnswered, string userName)
		{
			Mapper.CreateMap<Voting, VotingPageModel>()
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.TotalVotes, mo => mo.MapFrom(s => s.Questions.First().Answers.Count))
				.ForMember(d => d.Comments, mo => mo.Ignore())
				.ForMember(d => d.StartDate, mo => mo.MapFrom(s => s.StartDate))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreateDate))
				.ForMember(d => d.Status, mo => mo.MapFrom(s => s.StartDate.Date <= DateTime.Today && s.FinishTime.Date >= DateTime.Today ? s.Status : VotingStatusType.Closed))
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

			return Mapper.Map<Voting, VotingPageModel>(voting);
		}

		public static UserModel ToUserModel(this MembershipUser user, UserProfile userProfile = null)
		{
			Mapper.CreateMap<MembershipUser, UserModel>()
				.ForMember(d => d.UserId, mo => mo.MapFrom(s => (int)s.ProviderUserKey))
				.ForMember(d => d.CreateDate, mo => mo.MapFrom(s => s.CreationDate))
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
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.VotingId))
				.ForMember(d => d.VotingName, mo => mo.MapFrom(s => s.Voting.VotingName))
				.ForMember(d => d.CreatedBy, mo => mo.MapFrom(s => s.User.UserName))
				.ForMember(d => d.PictureUrl, mo => mo.MapFrom(s => s.User.UserProfile.PictureUrl ?? GlobalVariables.DefaultImagePath))
				.ForMember(d => d.Own, mo => mo.Ignore())
				.AfterMap((s, d) => d.Own = s.User.UserName.Equals(currentUserName));

			return Mapper.Map<Comment, CommentModel>(comment);
		}

		public static AnswerModel ToAnswerModel(this Answer answer, string pictureUrl = "")
		{
			Mapper.CreateMap<Answer, AnswerModel>()
				.ForMember(d => d.AnswerText, mo => mo.MapFrom(s => s.FixedAnswer == null ? s.AnswerText : s.FixedAnswer.AnswerText))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.Question.VotingId))
				.ForMember(d => d.VotingName, mo => mo.MapFrom(s => s.Question.Voting.VotingName))
				.AfterMap((s, d) => d.PictureUrl = pictureUrl);

			return Mapper.Map<Answer, AnswerModel>(answer);
		}

		public static QuestionModel ToQuestionModel(this Question question)
		{
			Mapper.CreateMap<Question, QuestionModel>()
				.ForMember(d => d.QuestionId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.VotingId))
				.ForMember(d => d.FixedAnswers,
					mo => mo.MapFrom(s => s.FixedAnswers.Select(fa => fa.ToFixedAnswerModel()).ToList()))
				.ForMember(d => d.Answers, mo => mo.Ignore());

			return Mapper.Map<Question, QuestionModel>(question);
		}

		public static QuestionModel ToQuestionModel(this Question question, int userId)
		{
			Mapper.CreateMap<Question, QuestionModel>()
				.ForMember(d => d.QuestionId, mo => mo.MapFrom(s => s.Id))
				.ForMember(d => d.VotingId, mo => mo.MapFrom(s => s.VotingId))
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

		public static string ToDefaultFormatString(this DateTime date)
		{
			return date.ToString(Resources.Resource.DateStringFormat);
		}

		public static string ToFullDefaultFormatString(this DateTime date)
		{
			return date.ToString(Resources.Resource.FullDateStringFormat);
		}
	}
}