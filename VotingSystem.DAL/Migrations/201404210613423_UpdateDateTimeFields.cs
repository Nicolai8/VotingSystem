using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
    public partial class UpdateDateTimeFields : DbMigration
    {
        public override void Up()
        {
			AlterColumn("dbo.Answer", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
			AlterColumn("dbo.Theme", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
			AlterColumn("dbo.Comment", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
			AlterColumn("dbo.User", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
			AlterColumn("dbo.Log", "Date", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
        }
    }
}
