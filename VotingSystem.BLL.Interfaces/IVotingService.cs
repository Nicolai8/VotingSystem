using System.Collections.Generic;
using VotingSystem.Common.Filters;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;

namespace VotingSystem.BLL.Interfaces
{
	public interface IVotingService
	{
		void InsertVoting(Voting voting);

		void DeleteVoting(int votingId);

		bool IsVotingClosed(int votingId);

		Voting GetVotingById(int votingId);

		Voting GetVotingByQuestionId(int questionId);

		List<Voting> GetVotingsByUserId(string query, int userId, Filter<Voting> filter);

		List<Voting> GetAllVotings(string query, Filter<Voting> filterExtended);

		List<Voting> GetAllActiveVotings(string query, Filter<Voting> filter);

		int GetNumberOfVotingsByVotingName(string partOfVotingName);

		int GetNumberOfActiveVotingsByVotingName(string partOfVotingName);

		int GetNumberOfUserVotings(int userId, string partOfVotingName);

		void UpdateVotingStatus(int votingId, VotingStatusType status);
	}
}