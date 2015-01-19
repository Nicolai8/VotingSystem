using System;
using VotingSystem.DAL.Enums;

namespace VotingSystem.DAL.Entities
{
	public class UserProfile : BaseEntity
	{
		public string PictureUrl { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public PrivacyType Privacy { get; set; }
		public bool SuggestedToBlock { get; set; }

		public virtual User User { get; set; }
	}
}
