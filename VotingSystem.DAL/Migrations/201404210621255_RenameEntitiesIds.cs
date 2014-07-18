using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	public partial class RenameEntitiesIds : DbMigration
	{
		public override void Up()
		{
			RenameColumn(table: "dbo.Answer", name: "AnswerId", newName: "Id");
			RenameColumn(table: "dbo.Comment", name: "CommentId", newName: "Id");
			RenameColumn(table: "dbo.Theme", name: "ThemeId", newName: "Id");
			RenameColumn(table: "dbo.User", name: "UserId", newName: "Id");
			RenameColumn(table: "dbo.Role", name: "RoleId", newName: "Id");
			RenameColumn(table: "dbo.Log", name: "LogId", newName: "Id");
			RenameColumn(table: "dbo.UserProfile", name: "UserId", newName: "Id");
		}

		public override void Down()
		{

		}
	}
}
