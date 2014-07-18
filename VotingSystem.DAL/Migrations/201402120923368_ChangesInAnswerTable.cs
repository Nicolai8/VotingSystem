using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class ChangesInAnswerTable : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.Answer", "UserId", "dbo.User");
			DropIndex("dbo.Answer", new[] { "UserId" });
			AlterColumn("dbo.Answer", "UserId", c => c.Int());
			CreateIndex("dbo.Answer", "UserId");
			AddForeignKey("dbo.Answer", "UserId", "dbo.User", "UserId");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Answer", "UserId", "dbo.User");
			DropIndex("dbo.Answer", new[] { "UserId" });
			AlterColumn("dbo.Answer", "UserId", c => c.Int(nullable: false));
			CreateIndex("dbo.Answer", "UserId");
			AddForeignKey("dbo.Answer", "UserId", "dbo.User", "UserId", cascadeDelete: true);
		}
	}
}
