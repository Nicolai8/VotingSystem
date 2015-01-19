using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	//REVIEW: It would be great if you could group methods bu functionality. For example or CRUD near each other. GetByWhatever near too
	public interface IUserService
	{
		//REVIEW: Rename?
		User GetUser(string userName);
		//REVIEW: Could Filter functionality be applied there?
		List<User> GetUsers(out int total, int page = 1, int pageSize = 10);
		//REVIEW: Could Filter functionality be applied there?
		List<User> GetSuggestedUsers(int page = 1, int pageSize = 10);

		User GetUserByEmail(string email);

		User GetUserById(int id);

		//REVIEW: Why user name is not unique?
		List<User> GetUsersByName(string userName);

		void UpdateUser(User user);

		void UpdateUser(string userName, string email = null, bool? isApproved = null, string password = null,
			string passwordQuestion = null, string passwordAnswer = null);

		void DeleteUser(string userName);

		void CreateUser(User user);

		bool IsUserInRole(string userName, string roleName);

		bool ToggleLock(string userName);

		//REVIEW: Rename?
		int GetTotal(Expression<Func<User, bool>> filter = null);

		string[] GetUserRoles(string userName);

		void RemoveUserFromAllRoles(string username);

		void RemoveUserFromRoles(string username, string[] roleNames);

		void AddUserToRoles(string username, string[] roleNames);
	}
}