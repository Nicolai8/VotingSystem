using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class ChangesForNeedsForMembershipProvider : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.User", "PasswordQuestion", c => c.String());
			AddColumn("dbo.User", "PasswordAnswer", c => c.String());
			AlterColumn("dbo.User", "CreateDate", c => c.DateTime(nullable: false));
			DropColumn("dbo.User", "DateOfBirth");
		}

		public override void Down()
		{
			AddColumn("dbo.User", "DateOfBirth", c => c.String());
			AlterColumn("dbo.User", "CreateDate", c => c.String());
			DropColumn("dbo.User", "PasswordAnswer");
			DropColumn("dbo.User", "PasswordQuestion");
		}
	}
}
