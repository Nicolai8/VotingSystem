using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.DAL.Entities
{
	public class Answer : BaseEntity
	{
		public int? UserId { get; set; }
		public int QuestionId { get; set; }
		public string AnswerText { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateDate { get; set; }
		public int? FixedAnswerId { get; set; }

		public virtual User User { get; set; }
		public virtual Question Question { get; set; }
		public virtual FixedAnswer FixedAnswer { get; set; }
	}
}
