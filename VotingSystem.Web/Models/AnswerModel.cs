using System;

namespace VotingSystem.Web.Models
{
	public class AnswerModel
	{
		public string AnswerText { get; set; }
		public DateTime CreateDate { get; set; }
		public string PictureUrl { get; set; }
		public int Count { get; set; }
		public bool IsUserAnswer { get; set; }
		public int VotingId { get; set; }
		public string VotingName { get; set; }
	}
}