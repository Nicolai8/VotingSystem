using System.Data.Entity.Migrations;
using VotingSystem.Common;

namespace VotingSystem.DAL.Migrations
{
    public partial class AddQuestions : DbMigration
    {
        public override void Up()
        {
			Sql(@"DELETE FROM Theme
	DELETE FROM [User]
	DELETE FROM Answer
	DELETE FROM Comment
	DELETE FROM UserProfile
	DELETE FROM [Role]");

            DropForeignKey("dbo.Answer", "ThemeId", "dbo.Theme");
            DropForeignKey("dbo.FixedAnswer", "ThemeId", "dbo.Theme");
            DropIndex("dbo.Answer", new[] { "ThemeId" });
            DropIndex("dbo.FixedAnswer", new[] { "ThemeId" });
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        ThemeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Theme", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.ThemeId);
            
            AddColumn("dbo.Answer", "QuestionId", c => c.Int(nullable: false));
            AddColumn("dbo.FixedAnswer", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Answer", "QuestionId");
            CreateIndex("dbo.FixedAnswer", "QuestionId");
            AddForeignKey("dbo.Answer", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FixedAnswer", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
            DropColumn("dbo.Answer", "ThemeId");
            DropColumn("dbo.FixedAnswer", "ThemeId");
            DropColumn("dbo.Theme", "VotingText");
            DropColumn("dbo.Theme", "Type");

			Sql(@"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeedTables]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SeedTables]");
			CreateStoredProcedure("SeedTables", string.Format(@"
	DELETE FROM Theme
	DELETE FROM [User]
	DELETE FROM Answer
	DELETE FROM Comment
	DELETE FROM UserProfile
	DELETE FROM [Role]
	DELETE FROM Question

--Create Users
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email1@email.com',getdate(),1,null,getdate(),'Admin',0,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email1@email.com',getdate(),1,null,getdate(),'User1',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email2@email.com',getdate(),1,null,getdate(),'User2',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email3@email.com',getdate(),1,null,getdate(),'User3',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email4@email.com',getdate(),1,null,getdate(),'User4',0,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email5@email.com',getdate(),1,null,getdate(),'User5',0,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email6@email.com',getdate(),1,null,getdate(),'User6',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email7@email.com',getdate(),1,null,getdate(),'User7',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email8@email.com',getdate(),1,null,getdate(),'User8',0,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email9@email.com',getdate(),1,null,getdate(),'User9',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email10@email.com',getdate(),1,null,getdate(),'User10',0,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email11@email.com',getdate(),1,null,getdate(),'User11',1,null,null)
	INSERT INTO [User]([Password],[Email],[CreateDate],[IsApproved],[ApproveCode],[ApproveSendDate],[UserName],[IsLocked],[PasswordQuestion],[PasswordAnswer]) VALUES ('{0}','email12@email.com',getdate(),1,null,getdate(),'User12',0,null,null)

--Create Roles
	INSERT INTO Role(RoleName) VALUES('Admin')
	INSERT INTO Role(RoleName) VALUES('User')
	INSERT INTO Role(RoleName) VALUES('Moderator')
		
--Set users variables
	DECLARE @adminId int = (SELECT id FROM [user] WHERE username='Admin');
	DECLARE @user1 int = (SELECT id FROM [user] WHERE username='User1');
	DECLARE @user2 int = (SELECT id FROM [user] WHERE username='User2');
	DECLARE @user3 int = (SELECT id FROM [user] WHERE username='User3');
	DECLARE @user4 int = (SELECT id FROM [user] WHERE username='User4');
	DECLARE @user5 int = (SELECT id FROM [user] WHERE username='User5');
	DECLARE @user6 int = (SELECT id FROM [user] WHERE username='User6');
	DECLARE @user7 int = (SELECT id FROM [user] WHERE username='User7');
	DECLARE @user8 int = (SELECT id FROM [user] WHERE username='User8');
	DECLARE @user9 int = (SELECT id FROM [user] WHERE username='User9');
	DECLARE @user10 int = (SELECT id FROM [user] WHERE username='User10');
	DECLARE @user11 int = (SELECT id FROM [user] WHERE username='User11');
	DECLARE @user12 int = (SELECT id FROM [user] WHERE username='User12');

		
--Create UserProfiles
	INSERT INTO UserProfile VALUES (@adminId,null,null,0,0)
	INSERT INTO UserProfile VALUES (@user1,null,null,0,0)
	INSERT INTO UserProfile VALUES (@user2,null,null,2,0)
	INSERT INTO UserProfile VALUES (@user3,null,null,1,0)
	INSERT INTO UserProfile VALUES (@user4,null,null,0,0)
	INSERT INTO UserProfile VALUES (@user5,null,null,2,0)
	INSERT INTO UserProfile VALUES (@user6,null,null,1,0)
	INSERT INTO UserProfile VALUES (@user7,null,null,1,0)
	INSERT INTO UserProfile VALUES (@user8,null,null,2,0)
	INSERT INTO UserProfile VALUES (@user9,null,null,1,0)
	INSERT INTO UserProfile VALUES (@user10,null,null,0,0)
	INSERT INTO UserProfile VALUES (@user11,null,null,1,0)
	INSERT INTO UserProfile VALUES (@user12,null,null,1,0)	

--Add users to roles
	INSERT INTO UserInRoles VALUES(@adminId,(SELECT Id FROM [role] WHERE rolename='Admin'))
	INSERT INTO UserInRoles VALUES(@user1,(SELECT Id FROM [role] WHERE rolename='Admin'))
	INSERT INTO UserInRoles VALUES(@user2,(SELECT Id FROM [role] WHERE rolename='User'))
	INSERT INTO UserInRoles VALUES(@user3,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user4,(SELECT Id FROM [role] WHERE rolename='User'))
	INSERT INTO UserInRoles VALUES(@user5,(SELECT Id FROM [role] WHERE rolename='User'))
	INSERT INTO UserInRoles VALUES(@user6,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user7,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user8,(SELECT Id FROM [role] WHERE rolename='Admin'))
	INSERT INTO UserInRoles VALUES(@user9,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user10,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user11,(SELECT Id FROM [role] WHERE rolename='Moderator'))
	INSERT INTO UserInRoles VALUES(@user12,(SELECT Id FROM [role] WHERE rolename='Admin'))

--Create themes
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'1Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.1',1,'Voting 1')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user3,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'2Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.2',3,'Voting 2')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'3Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.3',1,'Voting 3')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user8,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'4Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.4',2,'Voting 4')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'5Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.5',3,'Voting 5')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user1,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'6Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.6',2,'Voting 6')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user1,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'7Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.7',1,'Voting 7')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'8Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.8',1,'Voting 8')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user6,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'9Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.9',1,'Voting 9')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'10Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.10',2,'Voting 10')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user9,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'11Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.11',1,'Voting 11')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'12Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.12',3,'Voting 12')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user8,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'13Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.13',0,'Voting 13')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'14Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.14',2,'Voting 14')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'15Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.15',3,'Voting 15')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user7,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'16Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.16',0,'Voting 16')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'17Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.17',0,'Voting 17')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user1,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'18Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.18',2,'Voting 18')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user4,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'19Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.19',2,'Voting 19')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user4,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'20Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.20',1,'Voting 20')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user6,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'21Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.21',2,'Voting 21')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'22Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.22',3,'Voting 22')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'23Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.23',1,'Voting 23')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user4,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'24Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.24',3,'Voting 24')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'25Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.25',3,'Voting 25')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'26Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.26',0,'Voting 26')
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[Status],[VotingName]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'27Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.27',0,'Voting 27')

--set themes variables
	DECLARE @voting1 int = (SELECT id FROM theme WHERE votingName='Voting 1');
	DECLARE @voting2 int = (SELECT id FROM theme WHERE votingName='Voting 2');
	DECLARE @voting3 int = (SELECT id FROM theme WHERE votingName='Voting 3');
	DECLARE @voting4 int = (SELECT id FROM theme WHERE votingName='Voting 4');
	DECLARE @voting5 int = (SELECT id FROM theme WHERE votingName='Voting 5');
	DECLARE @voting6 int = (SELECT id FROM theme WHERE votingName='Voting 6');
	DECLARE @voting7 int = (SELECT id FROM theme WHERE votingName='Voting 7');
	DECLARE @voting8 int = (SELECT id FROM theme WHERE votingName='Voting 8');
	DECLARE @voting9 int = (SELECT id FROM theme WHERE votingName='Voting 9');
	DECLARE @voting10 int = (SELECT id FROM theme WHERE votingName='Voting 10');
	DECLARE @voting11 int = (SELECT id FROM theme WHERE votingName='Voting 11');
	DECLARE @voting12 int = (SELECT id FROM theme WHERE votingName='Voting 12');
	DECLARE @voting13 int = (SELECT id FROM theme WHERE votingName='Voting 13');
	DECLARE @voting14 int = (SELECT id FROM theme WHERE votingName='Voting 14');
	DECLARE @voting15 int = (SELECT id FROM theme WHERE votingName='Voting 15');
	DECLARE @voting16 int = (SELECT id FROM theme WHERE votingName='Voting 16');
	DECLARE @voting17 int = (SELECT id FROM theme WHERE votingName='Voting 17');
	DECLARE @voting18 int = (SELECT id FROM theme WHERE votingName='Voting 18');
	DECLARE @voting19 int = (SELECT id FROM theme WHERE votingName='Voting 19');
	DECLARE @voting20 int = (SELECT id FROM theme WHERE votingName='Voting 20');
	DECLARE @voting21 int = (SELECT id FROM theme WHERE votingName='Voting 21');
	DECLARE @voting22 int = (SELECT id FROM theme WHERE votingName='Voting 22');
	DECLARE @voting23 int = (SELECT id FROM theme WHERE votingName='Voting 23');
	DECLARE @voting24 int = (SELECT id FROM theme WHERE votingName='Voting 24');
	DECLARE @voting25 int = (SELECT id FROM theme WHERE votingName='Voting 25');
	DECLARE @voting26 int = (SELECT id FROM theme WHERE votingName='Voting 26');
	DECLARE @voting27 int = (SELECT id FROM theme WHERE votingName='Voting 27');
	
--Add comments
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting27,getdate(),'Comment Text 787515')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting16,getdate(),'Comment Text 398725')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting20,getdate(),'Comment Text 193615')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting20,getdate(),'Comment Text 65694')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting23,getdate(),'Comment Text 151209')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting12,getdate(),'Comment Text 884478')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting8,getdate(),'Comment Text 440004')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting13,getdate(),'Comment Text 383070')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting21,getdate(),'Comment Text 418178')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting24,getdate(),'Comment Text 711805')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting12,getdate(),'Comment Text 547518')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting14,getdate(),'Comment Text 791991')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting21,getdate(),'Comment Text 278181')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting14,getdate(),'Comment Text 236693')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting23,getdate(),'Comment Text 674232')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting2,getdate(),'Comment Text 305647')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting14,getdate(),'Comment Text 498681')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting20,getdate(),'Comment Text 916316')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting13,getdate(),'Comment Text 871673')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting13,getdate(),'Comment Text 863798')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting7,getdate(),'Comment Text 465209')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting24,getdate(),'Comment Text 822798')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting21,getdate(),'Comment Text 434248')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting25,getdate(),'Comment Text 669810')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting24,getdate(),'Comment Text 395168')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting20,getdate(),'Comment Text 219644')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting6,getdate(),'Comment Text 166396')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting10,getdate(),'Comment Text 921373')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting9,getdate(),'Comment Text 366427')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting14,getdate(),'Comment Text 564511')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting13,getdate(),'Comment Text 473361')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting4,getdate(),'Comment Text 150570')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting1,getdate(),'Comment Text 893876')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting2,getdate(),'Comment Text 862990')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting1,getdate(),'Comment Text 373573')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting20,getdate(),'Comment Text 317937')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting19,getdate(),'Comment Text 643156')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting2,getdate(),'Comment Text 521456')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting1,getdate(),'Comment Text 458575')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting1,getdate(),'Comment Text 104108')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting26,getdate(),'Comment Text 747235')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting11,getdate(),'Comment Text 866019')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting25,getdate(),'Comment Text 545837')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting15,getdate(),'Comment Text 145051')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting21,getdate(),'Comment Text 913251')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting22,getdate(),'Comment Text 277799')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting13,getdate(),'Comment Text 839094')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting2,getdate(),'Comment Text 325059')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting12,getdate(),'Comment Text 156540')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting10,getdate(),'Comment Text 592624')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting9,getdate(),'Comment Text 460555')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting20,getdate(),'Comment Text 336135')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting6,getdate(),'Comment Text 998099')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting4,getdate(),'Comment Text 345447')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting9,getdate(),'Comment Text 78516')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting1,getdate(),'Comment Text 817630')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting24,getdate(),'Comment Text 399426')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting9,getdate(),'Comment Text 253539')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting5,getdate(),'Comment Text 870375')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting10,getdate(),'Comment Text 42907')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting21,getdate(),'Comment Text 817501')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting20,getdate(),'Comment Text 639710')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting7,getdate(),'Comment Text 236587')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting6,getdate(),'Comment Text 42916')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting3,getdate(),'Comment Text 628571')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting23,getdate(),'Comment Text 431605')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting20,getdate(),'Comment Text 264965')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting26,getdate(),'Comment Text 449786')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting26,getdate(),'Comment Text 893382')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting3,getdate(),'Comment Text 349541')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting9,getdate(),'Comment Text 22929')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting20,getdate(),'Comment Text 123503')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting8,getdate(),'Comment Text 214237')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting15,getdate(),'Comment Text 590114')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting26,getdate(),'Comment Text 93638')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting7,getdate(),'Comment Text 353903')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting22,getdate(),'Comment Text 38420')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting27,getdate(),'Comment Text 1467')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting8,getdate(),'Comment Text 347130')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting3,getdate(),'Comment Text 79936')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting11,getdate(),'Comment Text 77300')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting20,getdate(),'Comment Text 492553')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting1,getdate(),'Comment Text 467583')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting5,getdate(),'Comment Text 607518')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting21,getdate(),'Comment Text 758101')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting4,getdate(),'Comment Text 899169')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting16,getdate(),'Comment Text 470146')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting21,getdate(),'Comment Text 125747')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting1,getdate(),'Comment Text 72737')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting19,getdate(),'Comment Text 464486')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting18,getdate(),'Comment Text 233985')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting4,getdate(),'Comment Text 538607')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting22,getdate(),'Comment Text 592594')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting20,getdate(),'Comment Text 49282')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting16,getdate(),'Comment Text 4459')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting8,getdate(),'Comment Text 400891')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting12,getdate(),'Comment Text 279505')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting23,getdate(),'Comment Text 822593')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting8,getdate(),'Comment Text 904546')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting14,getdate(),'Comment Text 298733')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting9,getdate(),'Comment Text 218134')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting24,getdate(),'Comment Text 899548')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting22,getdate(),'Comment Text 496912')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting8,getdate(),'Comment Text 409475')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting7,getdate(),'Comment Text 335489')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting1,getdate(),'Comment Text 474638')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting26,getdate(),'Comment Text 869520')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting27,getdate(),'Comment Text 842169')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting19,getdate(),'Comment Text 562502')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting23,getdate(),'Comment Text 91970')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting17,getdate(),'Comment Text 774325')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting7,getdate(),'Comment Text 55826')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting6,getdate(),'Comment Text 727423')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting2,getdate(),'Comment Text 678794')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting4,getdate(),'Comment Text 296480')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting17,getdate(),'Comment Text 29968')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting27,getdate(),'Comment Text 700183')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting24,getdate(),'Comment Text 115409')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting10,getdate(),'Comment Text 313328')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting27,getdate(),'Comment Text 23752')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting16,getdate(),'Comment Text 723194')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting11,getdate(),'Comment Text 442675')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting18,getdate(),'Comment Text 670020')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting25,getdate(),'Comment Text 325372')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting10,getdate(),'Comment Text 354613')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting1,getdate(),'Comment Text 393797')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting19,getdate(),'Comment Text 753687')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting15,getdate(),'Comment Text 775773')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting18,getdate(),'Comment Text 711124')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting8,getdate(),'Comment Text 749478')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting15,getdate(),'Comment Text 570371')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting23,getdate(),'Comment Text 796068')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting19,getdate(),'Comment Text 867091')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting25,getdate(),'Comment Text 662394')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting6,getdate(),'Comment Text 911726')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting9,getdate(),'Comment Text 728071')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting8,getdate(),'Comment Text 635054')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting19,getdate(),'Comment Text 624291')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting1,getdate(),'Comment Text 935859')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting8,getdate(),'Comment Text 397999')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting13,getdate(),'Comment Text 988177')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting11,getdate(),'Comment Text 692650')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting21,getdate(),'Comment Text 643494')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting24,getdate(),'Comment Text 833658')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting12,getdate(),'Comment Text 615353')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting26,getdate(),'Comment Text 835396')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting8,getdate(),'Comment Text 219672')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting13,getdate(),'Comment Text 661409')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting9,getdate(),'Comment Text 76545')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting11,getdate(),'Comment Text 18285')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting19,getdate(),'Comment Text 314997')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting22,getdate(),'Comment Text 582168')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting8,getdate(),'Comment Text 466092')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting23,getdate(),'Comment Text 835454')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting2,getdate(),'Comment Text 368983')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting4,getdate(),'Comment Text 347364')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting18,getdate(),'Comment Text 230752')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting20,getdate(),'Comment Text 348675')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting2,getdate(),'Comment Text 697242')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting11,getdate(),'Comment Text 536782')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting8,getdate(),'Comment Text 894306')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting19,getdate(),'Comment Text 844319')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting22,getdate(),'Comment Text 715515')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting4,getdate(),'Comment Text 629446')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting16,getdate(),'Comment Text 981925')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting24,getdate(),'Comment Text 460364')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting27,getdate(),'Comment Text 858749')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting18,getdate(),'Comment Text 457120')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting2,getdate(),'Comment Text 222469')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting2,getdate(),'Comment Text 176966')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting13,getdate(),'Comment Text 222484')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting4,getdate(),'Comment Text 179556')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting25,getdate(),'Comment Text 527983')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting8,getdate(),'Comment Text 304322')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting5,getdate(),'Comment Text 523660')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting14,getdate(),'Comment Text 83800')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting14,getdate(),'Comment Text 219')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting4,getdate(),'Comment Text 401078')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting17,getdate(),'Comment Text 847911')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting23,getdate(),'Comment Text 546351')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting22,getdate(),'Comment Text 133407')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting23,getdate(),'Comment Text 49932')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting2,getdate(),'Comment Text 379940')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting19,getdate(),'Comment Text 152984')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting23,getdate(),'Comment Text 849789')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting24,getdate(),'Comment Text 51416')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting25,getdate(),'Comment Text 989627')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting23,getdate(),'Comment Text 579308')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting10,getdate(),'Comment Text 826663')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting3,getdate(),'Comment Text 93494')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting17,getdate(),'Comment Text 820874')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting23,getdate(),'Comment Text 189140')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting13,getdate(),'Comment Text 356817')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting24,getdate(),'Comment Text 790286')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting8,getdate(),'Comment Text 859574')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting14,getdate(),'Comment Text 931082')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting5,getdate(),'Comment Text 418250')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting19,getdate(),'Comment Text 306412')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting7,getdate(),'Comment Text 588154')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting14,getdate(),'Comment Text 941030')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting6,getdate(),'Comment Text 547969')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting1,getdate(),'Comment Text 934392')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting26,getdate(),'Comment Text 617981')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting9,getdate(),'Comment Text 209858')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting3,getdate(),'Comment Text 370213')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting16,getdate(),'Comment Text 860485')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting25,getdate(),'Comment Text 204633')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting8,getdate(),'Comment Text 931979')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting19,getdate(),'Comment Text 775539')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting19,getdate(),'Comment Text 615326')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting2,getdate(),'Comment Text 346886')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting26,getdate(),'Comment Text 886929')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting15,getdate(),'Comment Text 812970')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting23,getdate(),'Comment Text 265595')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting16,getdate(),'Comment Text 252426')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user4,@voting4,getdate(),'Comment Text 213733')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user1,@voting12,getdate(),'Comment Text 663616')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting21,getdate(),'Comment Text 423872')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user3,@voting20,getdate(),'Comment Text 579505')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting25,getdate(),'Comment Text 977280')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting9,getdate(),'Comment Text 243255')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user2,@voting23,getdate(),'Comment Text 34497')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting1,getdate(),'Comment Text 121276')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting25,getdate(),'Comment Text 243852')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting11,getdate(),'Comment Text 913727')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting12,getdate(),'Comment Text 178912')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting24,getdate(),'Comment Text 909414')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting21,getdate(),'Comment Text 228536')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting21,getdate(),'Comment Text 242611')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user7,@voting8,getdate(),'Comment Text 327473')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting9,getdate(),'Comment Text 219319')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting25,getdate(),'Comment Text 982832')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting25,getdate(),'Comment Text 223233')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user6,@voting27,getdate(),'Comment Text 356545')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user8,@voting21,getdate(),'Comment Text 352854')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user10,@voting26,getdate(),'Comment Text 448898')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting13,getdate(),'Comment Text 898157')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user11,@voting10,getdate(),'Comment Text 38864')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user12,@voting9,getdate(),'Comment Text 583532')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting8,getdate(),'Comment Text 175547')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user5,@voting18,getdate(),'Comment Text 306494')
	INSERT INTO COMMENT([UserId],[ThemeId],[CreateDate],[CommentText]) VALUES(@user9,@voting17,getdate(),'Comment Text 882076')

--Add questions
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 1',0,@voting1)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 2',0,@voting1)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 3',0,@voting1)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 4',0,@voting2)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 5',0,@voting2)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 6',0,@voting2)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 7',0,@voting27)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 8',0,@voting20)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 9',0,@voting3)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 10',0,@voting21)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 11',0,@voting4)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 12',0,@voting26)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 13',0,@voting5)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 14',0,@voting5)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 15',0,@voting22)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 16',0,@voting6)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 17',0,@voting23)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 18',0,@voting7)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 19',0,@voting8)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 20',0,@voting8)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 21',0,@voting9)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 22',0,@voting9)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 23',0,@voting10)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 24',0,@voting24)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 25',0,@voting10)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 26',1,@voting11)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 27',1,@voting25)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 28',1,@voting12)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 29',1,@voting13)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 30',1,@voting14)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 31',1,@voting15)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 32',1,@voting15)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 33',1,@voting16)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 34',1,@voting17)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 35',1,@voting17)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 36',1,@voting18)
	INSERT INTO Question([Text],[Type],[ThemeId]) VALUES('Question Text 37',0,@voting19)

--Declare questions
	DECLARE @question1 int = (SELECT id FROM question WHERE Text ='Question Text 1');
	DECLARE @question2 int = (SELECT id FROM question WHERE Text ='Question Text 2');
	DECLARE @question3 int = (SELECT id FROM question WHERE Text ='Question Text 3');
	DECLARE @question4 int = (SELECT id FROM question WHERE Text ='Question Text 4');
	DECLARE @question5 int = (SELECT id FROM question WHERE Text ='Question Text 5');
	DECLARE @question6 int = (SELECT id FROM question WHERE Text ='Question Text 6');
	DECLARE @question7 int = (SELECT id FROM question WHERE Text ='Question Text 7');
	DECLARE @question8 int = (SELECT id FROM question WHERE Text ='Question Text 8');
	DECLARE @question9 int = (SELECT id FROM question WHERE Text ='Question Text 9');
	DECLARE @question10 int = (SELECT id FROM question WHERE Text ='Question Text 10');
	DECLARE @question11 int = (SELECT id FROM question WHERE Text ='Question Text 11');
	DECLARE @question12 int = (SELECT id FROM question WHERE Text ='Question Text 12');
	DECLARE @question13 int = (SELECT id FROM question WHERE Text ='Question Text 13');
	DECLARE @question14 int = (SELECT id FROM question WHERE Text ='Question Text 14');
	DECLARE @question15 int = (SELECT id FROM question WHERE Text ='Question Text 15');
	DECLARE @question16 int = (SELECT id FROM question WHERE Text ='Question Text 16');
	DECLARE @question17 int = (SELECT id FROM question WHERE Text ='Question Text 17');
	DECLARE @question18 int = (SELECT id FROM question WHERE Text ='Question Text 18');
	DECLARE @question19 int = (SELECT id FROM question WHERE Text ='Question Text 19');
	DECLARE @question20 int = (SELECT id FROM question WHERE Text ='Question Text 20');
	DECLARE @question21 int = (SELECT id FROM question WHERE Text ='Question Text 21');
	DECLARE @question22 int = (SELECT id FROM question WHERE Text ='Question Text 22');
	DECLARE @question23 int = (SELECT id FROM question WHERE Text ='Question Text 23');
	DECLARE @question24 int = (SELECT id FROM question WHERE Text ='Question Text 24');
	DECLARE @question25 int = (SELECT id FROM question WHERE Text ='Question Text 25');
	DECLARE @question26 int = (SELECT id FROM question WHERE Text ='Question Text 26');
	DECLARE @question27 int = (SELECT id FROM question WHERE Text ='Question Text 27');
	DECLARE @question28 int = (SELECT id FROM question WHERE Text ='Question Text 28');
	DECLARE @question29 int = (SELECT id FROM question WHERE Text ='Question Text 29');
	DECLARE @question30 int = (SELECT id FROM question WHERE Text ='Question Text 30');
	DECLARE @question31 int = (SELECT id FROM question WHERE Text ='Question Text 31');
	DECLARE @question32 int = (SELECT id FROM question WHERE Text ='Question Text 32');
	DECLARE @question33 int = (SELECT id FROM question WHERE Text ='Question Text 33');
	DECLARE @question34 int = (SELECT id FROM question WHERE Text ='Question Text 34');
	DECLARE @question35 int = (SELECT id FROM question WHERE Text ='Question Text 35');
	DECLARE @question36 int = (SELECT id FROM question WHERE Text ='Question Text 36');

--Create Answers
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user3,@question4,getdate(),'AnswerText 603476')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question20,getdate(),'AnswerText 203744')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question19,getdate(),'AnswerText 992117')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question1,getdate(),'AnswerText 817881')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question12,getdate(),'AnswerText 551781')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user2,@question2,getdate(),'AnswerText 6652')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user7,@question21,getdate(),'AnswerText 59707')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question11,getdate(),'AnswerText 271006')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user1,@question24,getdate(),'AnswerText 237206')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question21,getdate(),'AnswerText 827445')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question19,getdate(),'AnswerText 675869')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question8,getdate(),'AnswerText 327622')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user7,@question3,getdate(),'AnswerText 998348')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question6,getdate(),'AnswerText 117324')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user12,@question1,getdate(),'AnswerText 447531')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user7,@question25,getdate(),'AnswerText 526173')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user4,@question8,getdate(),'AnswerText 450618')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question6,getdate(),'AnswerText 620130')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question22,getdate(),'AnswerText 601320')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question11,getdate(),'AnswerText 936990')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question13,getdate(),'AnswerText 832121')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user6,@question12,getdate(),'AnswerText 90298')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question23,getdate(),'AnswerText 884317')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question17,getdate(),'AnswerText 504448')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question20,getdate(),'AnswerText 866563')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user7,@question19,getdate(),'AnswerText 463900')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question11,getdate(),'AnswerText 557982')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question23,getdate(),'AnswerText 88515')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user12,@question4,getdate(),'AnswerText 914823')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question5,getdate(),'AnswerText 659137')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question19,getdate(),'AnswerText 582908')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user1,@question23,getdate(),'AnswerText 891820')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user7,@question7,getdate(),'AnswerText 855560')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question6,getdate(),'AnswerText 855676')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user2,@question5,getdate(),'AnswerText 164238')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question5,getdate(),'AnswerText 969947')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question10,getdate(),'AnswerText 433584')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user6,@question11,getdate(),'AnswerText 343504')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user3,@question3,getdate(),'AnswerText 570550')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question11,getdate(),'AnswerText 141087')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question22,getdate(),'AnswerText 605914')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question21,getdate(),'AnswerText 940449')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user4,@question25,getdate(),'AnswerText 706835')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question18,getdate(),'AnswerText 115296')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question20,getdate(),'AnswerText 825346')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user2,@question3,getdate(),'AnswerText 939726')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question13,getdate(),'AnswerText 337158')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user1,@question7,getdate(),'AnswerText 738080')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question24,getdate(),'AnswerText 676990')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user12,@question8,getdate(),'AnswerText 259970')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question12,getdate(),'AnswerText 80982')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user4,@question16,getdate(),'AnswerText 711776')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user2,@question6,getdate(),'AnswerText 996595')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user1,@question18,getdate(),'AnswerText 294156')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user6,@question7,getdate(),'AnswerText 768283')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question15,getdate(),'AnswerText 663781')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user9,@question22,getdate(),'AnswerText 157129')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question10,getdate(),'AnswerText 371491')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user1,@question14,getdate(),'AnswerText 720160')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question10,getdate(),'AnswerText 778676')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user2,@question13,getdate(),'AnswerText 354552')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user5,@question24,getdate(),'AnswerText 674086')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question4,getdate(),'AnswerText 740268')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user10,@question24,getdate(),'AnswerText 589174')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user4,@question13,getdate(),'AnswerText 338847')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user8,@question15,getdate(),'AnswerText 935586')
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[AnswerText]) VALUES(@user11,@question24,getdate(),'AnswerText 407666')

--createFixedAnswers
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 395434')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 803824')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 422972')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 856792')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 644500')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question26,'AnswerText 408280')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question27,'AnswerText 530335')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question27,'AnswerText 974586')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question28,'AnswerText 772659')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question28,'AnswerText 435220')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question28,'AnswerText 538473')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question29,'AnswerText 423728')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question29,'AnswerText 482579')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question29,'AnswerText 105576')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question29,'AnswerText 810172')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question30,'AnswerText 243598')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question30,'AnswerText 699797')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question30,'AnswerText 998675')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question30,'AnswerText 920600')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question30,'AnswerText 52907')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question31,'AnswerText 38330')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question31,'AnswerText 118319')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question31,'AnswerText 388517')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question31,'AnswerText 183882')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question31,'AnswerText 824417')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question32,'AnswerText 13404')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question32,'AnswerText 711410')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question32,'AnswerText 644782')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question32,'AnswerText 202860')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question33,'AnswerText 43132')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question33,'AnswerText 817292')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question33,'AnswerText 858517')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question33,'AnswerText 68570')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question34,'AnswerText 228659')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question34,'AnswerText 217572')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question34,'AnswerText 279767')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question34,'AnswerText 634621')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question34,'AnswerText 887489')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 992407')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 252064')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 400300')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 945969')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 815863')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 579092')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question35,'AnswerText 280582')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question36,'AnswerText 859295')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question36,'AnswerText 758824')
	INSERT INTO FixedAnswer([QuestionId],[AnswerText]) VALUES(@question36,'AnswerText 971435')

--declare fixedAnswers Ids
	Declare @fixedAnswer1 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%395434%' and QuestionId=@question26);
	Declare @fixedAnswer2 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%803824%' and QuestionId=@question26);
	Declare @fixedAnswer3 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%422972%' and QuestionId=@question26);
	Declare @fixedAnswer4 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%856792%' and QuestionId=@question26);
	Declare @fixedAnswer5 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%644500%' and QuestionId=@question26);
	Declare @fixedAnswer6 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%408280%' and QuestionId=@question26);
	Declare @fixedAnswer7 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%530335%' and QuestionId=@question27);
	Declare @fixedAnswer8 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%974586%' and QuestionId=@question27);
	Declare @fixedAnswer9 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%772659%' and QuestionId=@question28);
	Declare @fixedAnswer10 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%435220%' and QuestionId=@question28);
	Declare @fixedAnswer11 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%538473%' and QuestionId=@question28);
	Declare @fixedAnswer12 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%423728%' and QuestionId=@question29);
	Declare @fixedAnswer13 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%482579%' and QuestionId=@question29);
	Declare @fixedAnswer14 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%105576%' and QuestionId=@question29);
	Declare @fixedAnswer15 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%810172%' and QuestionId=@question29);
	Declare @fixedAnswer16 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%243598%' and QuestionId=@question30);
	Declare @fixedAnswer17 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%699797%' and QuestionId=@question30);
	Declare @fixedAnswer18 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%998675%' and QuestionId=@question30);
	Declare @fixedAnswer19 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%920600%' and QuestionId=@question30);
	Declare @fixedAnswer20 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%52907%' and QuestionId=@question30);
	Declare @fixedAnswer21 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%38330%' and QuestionId=@question31);
	Declare @fixedAnswer22 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%118319%' and QuestionId=@question31);
	Declare @fixedAnswer23 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%388517%' and QuestionId=@question31);
	Declare @fixedAnswer24 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%183882%' and QuestionId=@question31);
	Declare @fixedAnswer25 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%824417%' and QuestionId=@question31);
	Declare @fixedAnswer26 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%13404%' and QuestionId=@question32);
	Declare @fixedAnswer27 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%711410%' and QuestionId=@question32);
	Declare @fixedAnswer28 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%644782%' and QuestionId=@question32);
	Declare @fixedAnswer29 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%202860%' and QuestionId=@question32);
	Declare @fixedAnswer30 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%43132%' and QuestionId=@question33);
	Declare @fixedAnswer31 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%817292%' and QuestionId=@question33);
	Declare @fixedAnswer32 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%858517%' and QuestionId=@question33);
	Declare @fixedAnswer33 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%68570%' and QuestionId=@question33);
	Declare @fixedAnswer34 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%228659%' and QuestionId=@question34);
	Declare @fixedAnswer35 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%217572%' and QuestionId=@question34);
	Declare @fixedAnswer36 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%279767%' and QuestionId=@question34);
	Declare @fixedAnswer37 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%634621%' and QuestionId=@question34);
	Declare @fixedAnswer38 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%887489%' and QuestionId=@question34);
	Declare @fixedAnswer39 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%992407%' and QuestionId=@question35);
	Declare @fixedAnswer40 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%252064%' and QuestionId=@question35);
	Declare @fixedAnswer41 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%400300%' and QuestionId=@question35);
	Declare @fixedAnswer42 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%945969%' and QuestionId=@question35);
	Declare @fixedAnswer43 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%815863%' and QuestionId=@question35);
	Declare @fixedAnswer44 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%579092%' and QuestionId=@question35);
	Declare @fixedAnswer45 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%280582%' and QuestionId=@question35);
	Declare @fixedAnswer46 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%859295%' and QuestionId=@question36);
	Declare @fixedAnswer47 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%758824%' and QuestionId=@question36);
	Declare @fixedAnswer48 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%971435%' and QuestionId=@question36);

--fixedAnswers
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@question26,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question26,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@question26,getdate(),@fixedAnswer4)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@question26,getdate(),@fixedAnswer3)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question26,getdate(),@fixedAnswer5)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@question26,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question26,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question26,getdate(),@fixedAnswer5)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@question26,getdate(),@fixedAnswer2)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question26,getdate(),@fixedAnswer6)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question27,getdate(),@fixedAnswer8)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question27,getdate(),@fixedAnswer7)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@question27,getdate(),@fixedAnswer8)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question27,getdate(),@fixedAnswer7)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question27,getdate(),@fixedAnswer7)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question27,getdate(),@fixedAnswer8)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question28,getdate(),@fixedAnswer11)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question28,getdate(),@fixedAnswer10)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question28,getdate(),@fixedAnswer10)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question28,getdate(),@fixedAnswer11)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question28,getdate(),@fixedAnswer10)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question28,getdate(),@fixedAnswer10)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question28,getdate(),@fixedAnswer9)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question28,getdate(),@fixedAnswer11)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@question28,getdate(),@fixedAnswer11)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question29,getdate(),@fixedAnswer14)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question29,getdate(),@fixedAnswer12)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question29,getdate(),@fixedAnswer13)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@question29,getdate(),@fixedAnswer14)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question29,getdate(),@fixedAnswer13)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@question29,getdate(),@fixedAnswer12)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question29,getdate(),@fixedAnswer15)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question29,getdate(),@fixedAnswer13)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question29,getdate(),@fixedAnswer13)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question30,getdate(),@fixedAnswer17)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question30,getdate(),@fixedAnswer20)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@question30,getdate(),@fixedAnswer18)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question30,getdate(),@fixedAnswer19)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@question30,getdate(),@fixedAnswer18)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question31,getdate(),@fixedAnswer21)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question31,getdate(),@fixedAnswer21)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question31,getdate(),@fixedAnswer21)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question31,getdate(),@fixedAnswer25)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question31,getdate(),@fixedAnswer23)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@question31,getdate(),@fixedAnswer24)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@question31,getdate(),@fixedAnswer24)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question32,getdate(),@fixedAnswer27)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question32,getdate(),@fixedAnswer26)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question32,getdate(),@fixedAnswer29)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question32,getdate(),@fixedAnswer28)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@question33,getdate(),@fixedAnswer37)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question33,getdate(),@fixedAnswer38)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question33,getdate(),@fixedAnswer38)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@question33,getdate(),@fixedAnswer38)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@question33,getdate(),@fixedAnswer34)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question34,getdate(),@fixedAnswer43)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@question34,getdate(),@fixedAnswer43)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@question34,getdate(),@fixedAnswer41)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question34,getdate(),@fixedAnswer41)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question34,getdate(),@fixedAnswer40)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question34,getdate(),@fixedAnswer39)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@question35,getdate(),@fixedAnswer45)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@question35,getdate(),@fixedAnswer41)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@question35,getdate(),@fixedAnswer44)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@question36,getdate(),@fixedAnswer47)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@question36,getdate(),@fixedAnswer46)
	INSERT INTO Answer([UserId],[QuestionId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@question36,getdate(),@fixedAnswer47)
"
			, SecurityHelper.CreateHash("password")));

			Sql("EXEC SeedTables");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Theme", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Theme", "VotingText", c => c.String(nullable: false));
            AddColumn("dbo.FixedAnswer", "ThemeId", c => c.Int(nullable: false));
            AddColumn("dbo.Answer", "ThemeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Question", "ThemeId", "dbo.Theme");
            DropForeignKey("dbo.FixedAnswer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.Question", new[] { "ThemeId" });
            DropIndex("dbo.FixedAnswer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropColumn("dbo.FixedAnswer", "QuestionId");
            DropColumn("dbo.Answer", "QuestionId");
            DropTable("dbo.Question");
            CreateIndex("dbo.FixedAnswer", "ThemeId");
            CreateIndex("dbo.Answer", "ThemeId");
            AddForeignKey("dbo.FixedAnswer", "ThemeId", "dbo.Theme", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answer", "ThemeId", "dbo.Theme", "Id", cascadeDelete: true);
        }
    }
}
