using System.Data.Entity.Migrations;
using VotingSystem.Common;

namespace VotingSystem.DAL.Migrations
{
    public partial class NewTypeOfQuestionChangeTypeToDate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FixedAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                        ThemeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Theme", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.ThemeId);
            
            AddColumn("dbo.Answer", "FixedAnswerId", c => c.Int());
            AddColumn("dbo.Theme", "Type", c => c.Int(nullable: false));
            CreateIndex("dbo.Answer", "FixedAnswerId");
            AddForeignKey("dbo.Answer", "FixedAnswerId", "dbo.FixedAnswer", "Id");

			Sql(@"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeedTables]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SeedTables]");
			CreateStoredProcedure("SeedTables", string.Format(@"
	DELETE FROM Theme
	DELETE FROM [User]
	DELETE FROM Answer
	DELETE FROM Comment
	DELETE FROM UserProfile
	DELETE FROM [Role]

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
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user1,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'1Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 1',2,'Voting 1',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user8,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'2Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 2',0,'Voting 2',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user3,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'3Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 3',1,'Voting 3',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'4Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 4',3,'Voting 4',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'5Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 5',0,'Voting 5',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user6,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'6Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 6',3,'Voting 6',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'7Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 7',0,'Voting 7',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'8Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 8',3,'Voting 8',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'9Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 9',0,'Voting 9',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'10Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 10',3,'Voting 10',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'11Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 11',2,'Voting 11',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user6,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'12Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 12',0,'Voting 12',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'13Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 13',1,'Voting 13',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user8,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'14Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 14',0,'Voting 14',0)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user9,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'15Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 15',0,'Voting 15',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'16Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 16',3,'Voting 16',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'17Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 17',1,'Voting 17',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user11,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'18Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 18',2,'Voting 18',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user5,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'19Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 19',1,'Voting 19',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user8,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'20Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 20',3,'Voting 20',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'21Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 21',0,'Voting 21',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'22Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 22',1,'Voting 22',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user12,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'23Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 23',1,'Voting 23',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user4,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'24Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 24',0,'Voting 24',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user2,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'25Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 25',3,'Voting 25',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user3,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'26Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 26',0,'Voting 26',1)
	INSERT INTO THEME([UserId],[CreateDate],[StartDate],[FinishTime],[Description],[VotingText],[Status],[VotingName],[Type]) VALUES(@user10,getdate(),(SELECT CONVERT(DATE, GETDATE())),DATEADD(day,7,(SELECT CONVERT(DATE, GETDATE()))),'27Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.','Voting Text 27',1,'Voting 27',1)

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

--Create Answers
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user11,@voting8,getdate(),'AnswerText 754090')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user9,@voting9,getdate(),'AnswerText 472411')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user1,@voting8,getdate(),'AnswerText 793455')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user8,@voting9,getdate(),'AnswerText 417816')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user9,@voting6,getdate(),'AnswerText 210107')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user2,@voting6,getdate(),'AnswerText 238325')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user11,@voting14,getdate(),'AnswerText 285907')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user4,@voting6,getdate(),'AnswerText 792296')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user6,@voting4,getdate(),'AnswerText 363937')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user2,@voting7,getdate(),'AnswerText 400770')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user1,@voting7,getdate(),'AnswerText 602569')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user11,@voting2,getdate(),'AnswerText 69409')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user1,@voting12,getdate(),'AnswerText 45598')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user1,@voting2,getdate(),'AnswerText 520175')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user3,@voting1,getdate(),'AnswerText 843538')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user8,@voting8,getdate(),'AnswerText 962886')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user3,@voting11,getdate(),'AnswerText 48141')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user2,@voting4,getdate(),'AnswerText 648036')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user5,@voting6,getdate(),'AnswerText 819895')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user12,@voting12,getdate(),'AnswerText 936557')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user9,@voting14,getdate(),'AnswerText 525289')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user2,@voting4,getdate(),'AnswerText 535743')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user3,@voting14,getdate(),'AnswerText 729980')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user3,@voting12,getdate(),'AnswerText 758001')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user11,@voting4,getdate(),'AnswerText 381025')
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[AnswerText]) VALUES(@user4,@voting3,getdate(),'AnswerText 992622')

--createFixedAnswers
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting15,'AnswerText 505933')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting15,'AnswerText 471294')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 38330')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 530335')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 228659')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 217572')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 395434')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 772659')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 803824')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 243598')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting16,'AnswerText 704086')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 43132')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 699797')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 422972')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 859295')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 974586')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 285162')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting17,'AnswerText 856792')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting18,'AnswerText 435220')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting18,'AnswerText 232026')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting18,'AnswerText 992407')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting18,'AnswerText 252064')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting18,'AnswerText 998675')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting19,'AnswerText 911614')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting19,'AnswerText 758824')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting20,'AnswerText 13404')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting20,'AnswerText 108178')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting20,'AnswerText 634514')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting20,'AnswerText 423728')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 817292')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 400300')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 945969')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 971435')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 858517')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 603674')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting21,'AnswerText 216645')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting22,'AnswerText 482579')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting22,'AnswerText 279767')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting22,'AnswerText 638844')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting22,'AnswerText 345714')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 533708')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 657149')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 118319')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 538473')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 388517')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting23,'AnswerText 815863')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting24,'AnswerText 105576')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting24,'AnswerText 810172')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting25,'AnswerText 644500')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting25,'AnswerText 711410')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting25,'AnswerText 183882')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting25,'AnswerText 579092')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting25,'AnswerText 644782')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 408280')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 202860')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 634621')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 887489')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 68570')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 728997')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 19928')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 809209')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting26,'AnswerText 162573')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting27,'AnswerText 920600')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting27,'AnswerText 52907')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting27,'AnswerText 874048')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting27,'AnswerText 280582')
	INSERT INTO FixedAnswer([ThemeId],[AnswerText]) VALUES(@voting27,'AnswerText 824417')

--declare fixedAnswers Ids
	Declare @fixedAnswer1 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%505933%' and ThemeId=@voting15);
	Declare @fixedAnswer2 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%471294%' and ThemeId=@voting15);
	Declare @fixedAnswer3 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%38330%' and ThemeId=@voting16);
	Declare @fixedAnswer4 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%530335%' and ThemeId=@voting16);
	Declare @fixedAnswer5 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%228659%' and ThemeId=@voting16);
	Declare @fixedAnswer6 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%217572%' and ThemeId=@voting16);
	Declare @fixedAnswer7 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%395434%' and ThemeId=@voting16);
	Declare @fixedAnswer8 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%772659%' and ThemeId=@voting16);
	Declare @fixedAnswer9 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%803824%' and ThemeId=@voting16);
	Declare @fixedAnswer10 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%243598%' and ThemeId=@voting16);
	Declare @fixedAnswer11 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%704086%' and ThemeId=@voting16);
	Declare @fixedAnswer12 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%43132%' and ThemeId=@voting17);
	Declare @fixedAnswer13 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%699797%' and ThemeId=@voting17);
	Declare @fixedAnswer14 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%422972%' and ThemeId=@voting17);
	Declare @fixedAnswer15 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%859295%' and ThemeId=@voting17);
	Declare @fixedAnswer16 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%974586%' and ThemeId=@voting17);
	Declare @fixedAnswer17 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%285162%' and ThemeId=@voting17);
	Declare @fixedAnswer18 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%856792%' and ThemeId=@voting17);
	Declare @fixedAnswer19 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%435220%' and ThemeId=@voting18);
	Declare @fixedAnswer20 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%232026%' and ThemeId=@voting18);
	Declare @fixedAnswer21 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%992407%' and ThemeId=@voting18);
	Declare @fixedAnswer22 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%252064%' and ThemeId=@voting18);
	Declare @fixedAnswer23 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%998675%' and ThemeId=@voting18);
	Declare @fixedAnswer24 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%911614%' and ThemeId=@voting19);
	Declare @fixedAnswer25 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%758824%' and ThemeId=@voting19);
	Declare @fixedAnswer26 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%13404%' and ThemeId=@voting20);
	Declare @fixedAnswer27 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%108178%' and ThemeId=@voting20);
	Declare @fixedAnswer28 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%634514%' and ThemeId=@voting20);
	Declare @fixedAnswer29 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%423728%' and ThemeId=@voting20);
	Declare @fixedAnswer30 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%817292%' and ThemeId=@voting21);
	Declare @fixedAnswer31 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%400300%' and ThemeId=@voting21);
	Declare @fixedAnswer32 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%945969%' and ThemeId=@voting21);
	Declare @fixedAnswer33 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%971435%' and ThemeId=@voting21);
	Declare @fixedAnswer34 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%858517%' and ThemeId=@voting21);
	Declare @fixedAnswer35 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%603674%' and ThemeId=@voting21);
	Declare @fixedAnswer36 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%216645%' and ThemeId=@voting21);
	Declare @fixedAnswer37 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%482579%' and ThemeId=@voting22);
	Declare @fixedAnswer38 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%279767%' and ThemeId=@voting22);
	Declare @fixedAnswer39 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%638844%' and ThemeId=@voting22);
	Declare @fixedAnswer40 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%345714%' and ThemeId=@voting22);
	Declare @fixedAnswer41 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%533708%' and ThemeId=@voting23);
	Declare @fixedAnswer42 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%657149%' and ThemeId=@voting23);
	Declare @fixedAnswer43 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%118319%' and ThemeId=@voting23);
	Declare @fixedAnswer44 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%538473%' and ThemeId=@voting23);
	Declare @fixedAnswer45 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%388517%' and ThemeId=@voting23);
	Declare @fixedAnswer46 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%815863%' and ThemeId=@voting23);
	Declare @fixedAnswer47 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%105576%' and ThemeId=@voting24);
	Declare @fixedAnswer48 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%810172%' and ThemeId=@voting24);
	Declare @fixedAnswer49 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%644500%' and ThemeId=@voting25);
	Declare @fixedAnswer50 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%711410%' and ThemeId=@voting25);
	Declare @fixedAnswer51 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%183882%' and ThemeId=@voting25);
	Declare @fixedAnswer52 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%579092%' and ThemeId=@voting25);
	Declare @fixedAnswer53 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%644782%' and ThemeId=@voting25);
	Declare @fixedAnswer54 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%408280%' and ThemeId=@voting26);
	Declare @fixedAnswer55 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%202860%' and ThemeId=@voting26);
	Declare @fixedAnswer56 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%634621%' and ThemeId=@voting26);
	Declare @fixedAnswer57 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%887489%' and ThemeId=@voting26);
	Declare @fixedAnswer58 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%68570%' and ThemeId=@voting26);
	Declare @fixedAnswer59 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%728997%' and ThemeId=@voting26);
	Declare @fixedAnswer60 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%19928%' and ThemeId=@voting26);
	Declare @fixedAnswer61 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%809209%' and ThemeId=@voting26);
	Declare @fixedAnswer62 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%162573%' and ThemeId=@voting26);
	Declare @fixedAnswer63 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%920600%' and ThemeId=@voting27);
	Declare @fixedAnswer64 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%52907%' and ThemeId=@voting27);
	Declare @fixedAnswer65 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%874048%' and ThemeId=@voting27);
	Declare @fixedAnswer66 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%280582%' and ThemeId=@voting27);
	Declare @fixedAnswer67 int = (SELECT Id FROM FixedAnswer WHERE AnswerText like '%824417%' and ThemeId=@voting27);

--fixedAnswers
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting15,getdate(),@fixedAnswer2)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting15,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@voting15,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting15,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting15,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@voting15,getdate(),@fixedAnswer1)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting15,getdate(),@fixedAnswer2)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting16,getdate(),@fixedAnswer7)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting16,getdate(),@fixedAnswer6)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting17,getdate(),@fixedAnswer13)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting17,getdate(),@fixedAnswer16)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting17,getdate(),@fixedAnswer16)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting18,getdate(),@fixedAnswer21)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting18,getdate(),@fixedAnswer21)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@voting18,getdate(),@fixedAnswer19)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@voting19,getdate(),@fixedAnswer25)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting19,getdate(),@fixedAnswer24)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@voting19,getdate(),@fixedAnswer25)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting19,getdate(),@fixedAnswer25)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting19,getdate(),@fixedAnswer25)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting20,getdate(),@fixedAnswer26)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting20,getdate(),@fixedAnswer29)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@voting20,getdate(),@fixedAnswer27)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting20,getdate(),@fixedAnswer27)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@voting21,getdate(),@fixedAnswer34)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@voting21,getdate(),@fixedAnswer31)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting21,getdate(),@fixedAnswer32)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting21,getdate(),@fixedAnswer33)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting21,getdate(),@fixedAnswer35)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting21,getdate(),@fixedAnswer34)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@voting21,getdate(),@fixedAnswer32)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting21,getdate(),@fixedAnswer35)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting21,getdate(),@fixedAnswer34)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@voting21,getdate(),@fixedAnswer34)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting22,getdate(),@fixedAnswer39)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user10,@voting22,getdate(),@fixedAnswer40)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting22,getdate(),@fixedAnswer40)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting23,getdate(),@fixedAnswer46)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user7,@voting23,getdate(),@fixedAnswer44)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@voting23,getdate(),@fixedAnswer43)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user4,@voting23,getdate(),@fixedAnswer45)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting24,getdate(),@fixedAnswer48)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting24,getdate(),@fixedAnswer47)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting24,getdate(),@fixedAnswer47)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting25,getdate(),@fixedAnswer52)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting25,getdate(),@fixedAnswer50)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting25,getdate(),@fixedAnswer52)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user11,@voting26,getdate(),@fixedAnswer55)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting26,getdate(),@fixedAnswer56)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@voting26,getdate(),@fixedAnswer60)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user1,@voting26,getdate(),@fixedAnswer57)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user12,@voting27,getdate(),@fixedAnswer67)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@voting27,getdate(),@fixedAnswer64)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user6,@voting27,getdate(),@fixedAnswer65)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user9,@voting27,getdate(),@fixedAnswer64)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user3,@voting27,getdate(),@fixedAnswer66)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user8,@voting27,getdate(),@fixedAnswer64)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user2,@voting27,getdate(),@fixedAnswer64)
	INSERT INTO Answer([UserId],[ThemeId],[CreateDate],[FixedAnswerId]) VALUES(@user5,@voting27,getdate(),@fixedAnswer65)
"
	, SecurityHelper.CreateHash("password")));

			Sql("EXEC SeedTables");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FixedAnswer", "ThemeId", "dbo.Theme");
            DropForeignKey("dbo.Answer", "FixedAnswerId", "dbo.FixedAnswer");
            DropIndex("dbo.FixedAnswer", new[] { "ThemeId" });
            DropIndex("dbo.Answer", new[] { "FixedAnswerId" });
            DropColumn("dbo.Theme", "Type");
            DropColumn("dbo.Answer", "FixedAnswerId");
            DropTable("dbo.FixedAnswer");
        }
    }
}
