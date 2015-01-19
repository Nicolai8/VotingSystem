using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.DAL.Entities
{
	public class User : BaseEntity
	{
		//REVIEW: What about adding unique constraint there?
		public string UserName { get; set; }
		public string Password { get; set; }
		//REVIEW: What about adding unique constraint there?
		public string Email { get; set; }
		public string PasswordQuestion { get; set; }
		public string PasswordAnswer { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreateDate { get; set; }

		public bool IsApproved { get; set; }
		public bool IsLocked { get; set; }
		public string ApproveCode { get; set; }
		public DateTime ApproveSendDate { get; set; }

		public virtual UserProfile UserProfile { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		public virtual ICollection<Theme> Themes { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
	}
}
