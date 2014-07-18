using VotingSystem.DAL;

namespace VotingSystem.BLL
{
	public abstract class BaseService
	{
		protected readonly IUnitOfWork UnitOfWork;

		protected BaseService(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}
	}
}
