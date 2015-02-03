using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Repositories;

namespace VotingSystem.DAL
{
	public interface IUnitOfWork
	{
		IGenericRepository<Answer> AnswerRepository { get; }

		IGenericRepository<Comment> CommentRepository { get; }

		IGenericRepository<Voting> VotingRepository { get; }

		IGenericRepository<User> UserRepository { get; }

		IGenericRepository<Role> RoleRepository { get; }

		IGenericRepository<UserProfile> UserProfileRepository { get; }

		void Save();
	}
}