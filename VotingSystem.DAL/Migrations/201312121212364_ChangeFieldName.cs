using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class ChangeFieldName : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.User", "ApproveSendDate", c => c.DateTime(nullable: false));
			DropColumn("dbo.User", "ApproveSedDate");
		}

		public override void Down()
		{
			AddColumn("dbo.User", "ApproveSedDate", c => c.DateTime(nullable: false));
			DropColumn("dbo.User", "ApproveSendDate");
		}
	}
}
