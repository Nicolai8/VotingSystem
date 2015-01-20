using System;
using VotingSystem.DAL.Enums;

namespace VotingSystem.Web.Models
{
	public class VotingModel
	{
		public int VotingId { get; set; }
		public string VotingName { get; set; }
		public int UserId { get; set; }
		public string CreatedBy { get; set; }
		public int AnswersCount { get; set; }
		public int CommentsCount { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public VotingStatusType? Status { get; set; }
		public DateTime CreateDate { get; set; }
		public string Description { get; set; }
	}
}