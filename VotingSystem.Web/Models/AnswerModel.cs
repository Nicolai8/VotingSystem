﻿namespace VotingSystem.Web.Models
{
	public class AnswerModel
	{
		public string AnswerText { get; set; }
		public string CreateDate { get; set; }
		public string PictureUrl { get; set; }
		public int Count { get; set; }
		public bool IsUserAnswer { get; set; }
	}
}