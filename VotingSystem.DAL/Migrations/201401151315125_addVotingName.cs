using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class addVotingName : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Theme", "VotingName", c => c.String());
		}

		public override void Down()
		{
			DropColumn("dbo.Theme", "VotingName");
		}
	}
}
