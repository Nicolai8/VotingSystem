﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.DAL.Entities
{
	public class Comment : BaseEntity
	{
		public Comment()
		{
			CreateDate = DateTime.UtcNow;
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateDate { get; set; }
		public string CommentText { get; set; }

		public int? UserId { get; set; }
		public virtual User User { get; set; }

		public int ThemeId { get; set; }
		public virtual Theme Theme { get; set; }
	}
}
