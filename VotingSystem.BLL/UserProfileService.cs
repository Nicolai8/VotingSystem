using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class UserProfileService : BaseService, IUserProfileService
	{
		public UserProfileService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public UserProfile GetUserProfileByUserId(int userId)
		{
			return UnitOfWork.UserProfileRepository.GetById(userId);
		}

		public void UpdateUserProfile(UserProfile userProfile)
		{
			UnitOfWork.UserProfileRepository.Update(userProfile);
			UnitOfWork.Save();
		}
	}
}
