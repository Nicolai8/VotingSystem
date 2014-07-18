using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class SetNotNullFieldsInTheme : DbMigration
	{
		public override void Up()
		{
			AlterColumn("dbo.Theme", "VotingName", c => c.String(nullable: false));
			AlterColumn("dbo.Theme", "VotingText", c => c.String(nullable: false));
		}

		public override void Down()
		{
			AlterColumn("dbo.Theme", "VotingText", c => c.String());
			AlterColumn("dbo.Theme", "VotingName", c => c.String());
		}
	}
}
