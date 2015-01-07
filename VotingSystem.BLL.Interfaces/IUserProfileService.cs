using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IUserProfileService
	{
		UserProfile GetByUserId(int userId);
		void Update(UserProfile userProfile);
	}
}
