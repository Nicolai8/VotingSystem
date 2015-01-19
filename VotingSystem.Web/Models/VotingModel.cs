using VotingSystem.DAL.Structures;

namespace VotingSystem.Web.Models
{
	public class VotingModel
	{
		public int VotingId { get; set; }
		public string VotingName { get; set; }
		public string CreatedBy { get; set; }
		public int AnswersCount { get; set; }
		public int CommentsCount { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public StatusType? Status { get; set; }
		public string CreateDate { get; set; }
		public string Description { get; set; }
	}
}