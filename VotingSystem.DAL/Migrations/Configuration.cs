using System.Data.Entity.Migrations;

namespace VotingSystem.DAL.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<VotingSystemContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(VotingSystemContext context)
		{
		}
	}
}
