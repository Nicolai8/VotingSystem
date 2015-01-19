using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotingSystem.DAL.Enums;

namespace VotingSystem.DAL.Entities
{
	public class Question : BaseEntity, IValidatableObject
	{
		[Required]
		public string Text { get; set; }
		public QuestionType Type { get; set; }

		public int ThemeId { get; set; }
		public virtual Theme Theme { get; set; }
	
		public virtual ICollection<Answer> Answers { get; set; }
		public virtual ICollection<FixedAnswer> FixedAnswers { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Type == QuestionType.ChoiceQuestion && FixedAnswers.Count < 2)
			{
				yield return new ValidationResult("Questions of \"ChoiceQuestion\" type should contain at least 2 predefined answeres.");
			}
		}
	}
}
