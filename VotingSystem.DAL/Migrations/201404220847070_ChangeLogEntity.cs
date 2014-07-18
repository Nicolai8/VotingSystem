using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class ChangeLogEntity : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Log", "Logger", c => c.String(maxLength: 255));
			AddColumn("dbo.Log", "Exception", c => c.String(maxLength: 2000));
			DropColumn("dbo.Log", "Source");
			DropColumn("dbo.Log", "Code");
		}

		public override void Down()
		{
			AddColumn("dbo.Log", "Code", c => c.String());
			AddColumn("dbo.Log", "Source", c => c.String());
			DropColumn("dbo.Log", "Exception");
			DropColumn("dbo.Log", "Logger");
		}
	}
}
