using System.Collections.Generic;

namespace VotingSystem.DAL.Entities
{
	public class FixedAnswer:BaseEntity
	{
		public string AnswerText { get; set; }

		public int QuestionId { get; set; }
		public virtual Question Question { get; set; }

		public virtual ICollection<Answer> Answers { get; set; }
	}
}
