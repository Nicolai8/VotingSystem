using System.Collections.Generic;
using VotingSystem.DAL.Enums;

namespace VotingSystem.Web.Models
{
	public class QuestionModel
	{
		public int? VotingId { get; set; }

		public int QuestionId { get; set; }
		public QuestionType Type { get; set; }

		public string Text { get; set; }

		public IEnumerable<AnswerModel> Answers { get; set; }
		public List<object> FixedAnswers { get; set; }
	}
}