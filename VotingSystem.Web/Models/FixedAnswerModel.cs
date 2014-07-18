namespace VotingSystem.Web.Models
{
	public class FixedAnswerModel
	{
		public int Id { get; set; }
		public string AnswerText { get; set; }
		public int QuestionId { get; set; }
	}
}