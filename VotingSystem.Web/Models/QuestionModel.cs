using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.Web.Models
{
	public class QuestionModel
	{
		public int QuestionId { get; set; }
		public int? VotingId { get; set; }
		public IEnumerable<AnswerModel> Answers { get; set; }
		public string Text { get; set; }
		public QuestionType Type { get; set; }
		public List<object> FixedAnswers { get; set; }
	}
}