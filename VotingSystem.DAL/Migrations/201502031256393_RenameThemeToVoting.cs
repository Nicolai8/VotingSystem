using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class RenameThemeToVoting : DbMigration
	{
		public override void Up()
		{
			RenameColumn(table: "dbo.Comment", name: "ThemeId", newName: "VotingId");
			RenameColumn(table: "dbo.Question", name: "ThemeId", newName: "VotingId");
			RenameTable("dbo.Theme", "Voting");
		}

		public override void Down()
		{
			RenameColumn(table: "dbo.Comment", name: "VotingId", newName: "ThemeId");
			RenameColumn(table: "dbo.Question", name: "VotingId", newName: "ThemeId");
			RenameTable("dbo.Voting", "Theme");
		}
	}
}
