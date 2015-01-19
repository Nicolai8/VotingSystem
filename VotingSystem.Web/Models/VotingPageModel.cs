using System.Collections.Generic;
using VotingSystem.DAL.Structures;

namespace VotingSystem.Web.Models
{
	public class VotingPageModel
	{
		public int VotingId { get; set; }
		public int TotalVotes { get; set; }
		public string VotingName { get; set; }
		public string CreatedBy { get; set; }
		public StatusType? Status { get; set; }
		public string StartDate { get; set; }
		public string TimeLeft { get; set; }
		public string Description { get; set; }
		public List<CommentModel> Comments { get; set; }
		public List<QuestionModel> Questions { get; set; }
		public string CreateDate { get; set; }
		public bool IsAnswered { get; set; }
	}
}