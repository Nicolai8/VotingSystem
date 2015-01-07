using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.DAL.Entities
{
	public class Answer : BaseEntity
	{
		public string AnswerText { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateDate { get; set; }

		//REVIEW: Combine Id and reference properties
		public int? UserId { get; set; }
		public virtual User User { get; set; }

		//REVIEW: Combine Id and reference properties
		public int QuestionId { get; set; }
		public virtual Question Question { get; set; }

		//REVIEW: Combine Id and reference properties
		public int? FixedAnswerId { get; set; }
		public virtual FixedAnswer FixedAnswer { get; set; }
	}
}
