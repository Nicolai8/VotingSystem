using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class CreateUserProfile : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.Comment", "UserId", "dbo.User");
			DropForeignKey("dbo.Theme", "UserId", "dbo.User");
			DropIndex("dbo.Comment", new[] { "UserId" });
			DropIndex("dbo.Theme", new[] { "UserId" });
			CreateTable(
				"dbo.UserProfile",
				c => new
					{
						UserId = c.Int(nullable: false),
						PictureUrl = c.String(),
						DateOfBirth = c.DateTime(),
						Privacy = c.Int(nullable: false),
						SuggestedToBlock = c.Boolean(nullable: false),
					})
				.PrimaryKey(t => t.UserId)
				.ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
				.Index(t => t.UserId);

			AlterColumn("dbo.Theme", "UserId", c => c.Int());
			AlterColumn("dbo.Comment", "UserId", c => c.Int());
			CreateIndex("dbo.Comment", "UserId");
			CreateIndex("dbo.Theme", "UserId");
			AddForeignKey("dbo.Comment", "UserId", "dbo.User", "UserId");
			AddForeignKey("dbo.Theme", "UserId", "dbo.User", "UserId");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Theme", "UserId", "dbo.User");
			DropForeignKey("dbo.Comment", "UserId", "dbo.User");
			DropForeignKey("dbo.UserProfile", "UserId", "dbo.User");
			DropIndex("dbo.Theme", new[] { "UserId" });
			DropIndex("dbo.Comment", new[] { "UserId" });
			DropIndex("dbo.UserProfile", new[] { "UserId" });
			AlterColumn("dbo.Comment", "UserId", c => c.Int(nullable: false));
			AlterColumn("dbo.Theme", "UserId", c => c.Int(nullable: false));
			DropTable("dbo.UserProfile");
			CreateIndex("dbo.Theme", "UserId");
			CreateIndex("dbo.Comment", "UserId");
			AddForeignKey("dbo.Theme", "UserId", "dbo.User", "UserId", cascadeDelete: true);
			AddForeignKey("dbo.Comment", "UserId", "dbo.User", "UserId", cascadeDelete: true);
		}
	}
}
