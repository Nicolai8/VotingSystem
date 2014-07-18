using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VotingSystem.DAL.Entities;

namespace VotingSystem.DAL
{
	public class VotingSystemContext : DbContext
	{
		public VotingSystemContext()
			: base("VotingSystemContext")
		{
		}

		public DbSet<Answer> Answers { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Theme> Themes { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<Log> Logs { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }

		public DbSet<Question> Questions { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder
				.Conventions
				.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<User>().
				HasMany(c => c.Roles).
				WithMany(p => p.Users).
				Map(
					m =>
					{
						m.MapLeftKey("UserId");
						m.MapRightKey("RoleId");
						m.ToTable("UserInRoles");
					});

			modelBuilder.Entity<User>()
				.HasRequired(u => u.UserProfile)
				.WithRequiredPrincipal(u => u.User)
				.WillCascadeOnDelete(true);
		}
	}
}
