using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.DAL.Entities
{
	public class Log : BaseEntity
    {
		[MaxLength(255)]
        public string Logger { get; set; }
		[MaxLength(4000)]
        public string Message { get; set; }
		[MaxLength(2000)]
		public string Exception { get; set; }
		[MaxLength(50)]
        public string Type { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]    
        public DateTime Date { get; set; }
    }
}
