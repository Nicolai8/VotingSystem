using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystem.DAL.Enums;

namespace VotingSystem.DAL.Entities
{
	public class Theme : BaseEntity
	{
		[Required]
		public string VotingName { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateDate { get; set; }
		[Required]
		[Column(TypeName = "date")]
		public DateTime StartDate { get; set; }
		[Required]
		[Column(TypeName = "date")]
		public DateTime FinishTime { get; set; }
		public string Description { get; set; }
		public StatusType? Status { get; set; }

		public int? UserId { get; set; }
		public virtual User User { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Question> Questions { get; set; }
	}
}
