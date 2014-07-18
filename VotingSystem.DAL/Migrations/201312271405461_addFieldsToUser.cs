using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class addFieldsToUser : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.User", "UserName", c => c.String());
			AddColumn("dbo.User", "IsLocked", c => c.Boolean(nullable: false));
		}

		public override void Down()
		{
			DropColumn("dbo.User", "IsLocked");
			DropColumn("dbo.User", "UserName");
		}
	}
}
