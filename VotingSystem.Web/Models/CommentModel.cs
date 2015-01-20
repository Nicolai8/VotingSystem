namespace VotingSystem.Web.Models
{
	public class CommentModel
	{
		public int CommentId { get; set; }
		public string CreateDate { get; set; }
		public string CommentText { get; set; }
		public int UserId { get; set; }
		public string CreatedBy { get; set; }
		public int? VotingId { get; set; }
		public string VotingName { get; set; }
		public bool Own { get; set; }
		public string PictureUrl { get; set; }
	}
}