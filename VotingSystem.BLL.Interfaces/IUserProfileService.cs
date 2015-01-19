using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IUserProfileService
	{
		//REVIEW: Possibly need to be renamed
		UserProfile GetByUserId(int userId);

		void UpdateUserProfile(UserProfile userProfile);
	}
}
