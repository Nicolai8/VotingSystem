using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class AnswerAddCreationDate : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Answer", "CreateDate", c => c.DateTime(nullable: false));
		}

		public override void Down()
		{
			DropColumn("dbo.Answer", "CreateDate");
		}
	}
}
