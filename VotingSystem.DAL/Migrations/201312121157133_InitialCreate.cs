using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class InitialCreate : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Answer",
				c => new
					{
						AnswerId = c.Int(nullable: false, identity: true),
						UserId = c.Int(nullable: false),
						ThemeId = c.Int(nullable: false),
						AnswerText = c.String(),
					})
				.PrimaryKey(t => t.AnswerId)
				.ForeignKey("dbo.Theme", t => t.ThemeId, cascadeDelete: true)
				.ForeignKey("dbo.User", t => t.UserId)
				.Index(t => t.ThemeId)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.Theme",
				c => new
					{
						ThemeId = c.Int(nullable: false, identity: true),
						UserId = c.Int(nullable: false),
						CreateDate = c.DateTime(nullable: false),
						StartDate = c.DateTime(nullable: false),
						FinishTime = c.DateTime(nullable: false),
						Description = c.String(),
						VotingText = c.String(),
						Status = c.Int(),
					})
				.PrimaryKey(t => t.ThemeId)
				.ForeignKey("dbo.User", t => t.UserId)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.Comment",
				c => new
					{
						CommentId = c.Int(nullable: false, identity: true),
						UserId = c.Int(nullable: false),
						ThemeId = c.Int(nullable: false),
						CreateDate = c.DateTime(nullable: false),
						CommentText = c.String(),
					})
				.PrimaryKey(t => t.CommentId)
				.ForeignKey("dbo.Theme", t => t.ThemeId, cascadeDelete: true)
				.ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
				.Index(t => t.ThemeId)
				.Index(t => t.UserId);

			CreateTable(
				"dbo.User",
				c => new
					{
						UserId = c.Int(nullable: false, identity: true),
						Password = c.String(),
						Email = c.String(),
						DateOfBirth = c.String(),
						CreateDate = c.String(),
						IsApproved = c.Boolean(nullable: false),
						ApproveCode = c.String(),
						ApproveSedDate = c.DateTime(nullable: false),
					})
				.PrimaryKey(t => t.UserId);

			CreateTable(
				"dbo.Role",
				c => new
					{
						RoleId = c.Int(nullable: false, identity: true),
						RoleName = c.String(),
					})
				.PrimaryKey(t => t.RoleId);

			CreateTable(
				"dbo.Log",
				c => new
					{
						LogId = c.Int(nullable: false, identity: true),
						Source = c.String(),
						Message = c.String(),
						Type = c.String(),
						Code = c.String(),
						Date = c.DateTime(nullable: false),
					})
				.PrimaryKey(t => t.LogId);

			CreateTable(
				"dbo.UserInRoles",
				c => new
					{
						UserId = c.Int(nullable: false),
						RoleId = c.Int(nullable: false),
					})
				.PrimaryKey(t => new { t.UserId, t.RoleId })
				.ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
				.ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
				.Index(t => t.UserId)
				.Index(t => t.RoleId);

		}

		public override void Down()
		{
			DropForeignKey("dbo.Theme", "UserId", "dbo.User");
			DropForeignKey("dbo.UserInRoles", "RoleId", "dbo.Role");
			DropForeignKey("dbo.UserInRoles", "UserId", "dbo.User");
			DropForeignKey("dbo.Comment", "UserId", "dbo.User");
			DropForeignKey("dbo.Answer", "UserId", "dbo.User");
			DropForeignKey("dbo.Comment", "ThemeId", "dbo.Theme");
			DropForeignKey("dbo.Answer", "ThemeId", "dbo.Theme");
			DropIndex("dbo.Theme", new[] { "UserId" });
			DropIndex("dbo.UserInRoles", new[] { "RoleId" });
			DropIndex("dbo.UserInRoles", new[] { "UserId" });
			DropIndex("dbo.Comment", new[] { "UserId" });
			DropIndex("dbo.Answer", new[] { "UserId" });
			DropIndex("dbo.Comment", new[] { "ThemeId" });
			DropIndex("dbo.Answer", new[] { "ThemeId" });
			DropTable("dbo.UserInRoles");
			DropTable("dbo.Log");
			DropTable("dbo.Role");
			DropTable("dbo.User");
			DropTable("dbo.Comment");
			DropTable("dbo.Theme");
			DropTable("dbo.Answer");
		}
	}
}
