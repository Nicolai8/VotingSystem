using System.ComponentModel.DataAnnotations;

namespace VotingSystem.DAL.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
