using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IUserProfileService
	{
		UserProfile GetUserProfileByUserId(int userId);

		void UpdateUserProfile(UserProfile userProfile);
	}
}
