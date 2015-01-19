using VotingSystem.DAL.Structures;

namespace VotingSystem.Web.Models
{
	public class UserModel
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string[] Roles { get; set; }
		public string Email { get; set; }
		public string CreateDate { get; set; }
		public bool IsBlocked { get; set; }
		public PrivacyType? Privacy { get; set; }
		public string PictureUrl { get; set; }
	}
}