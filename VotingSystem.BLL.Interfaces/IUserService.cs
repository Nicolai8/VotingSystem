using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IUserService
	{
		User GetUser(string userName);
		List<User> GetUsers(out int total, int page = 1, int pageSize = 10);
		List<User> GetSuggestedUsers(int page = 1, int pageSize = 10);
		User GetUserByEmail(string email);
		User GetUserById(int id);
		string[] GetUserRoles(string userName);
		List<User> GetUsersByName(string userName);
		void UpdateUser(User user);
		void UpdateUser(string userName, string email = null, bool? isApproved = null, string password = null,
			string passwordQuestion = null, string passwordAnswer = null);
		void DeleteUser(string userName);
		void CreateUser(User user);
		bool IsUserInRole(string userName, string roleName);
		bool ToggleLock(string userName);
		int GetTotal(Expression<Func<User, bool>> filter = null);
		void RemoveUserFromAllRoles(string username);
		void RemoveUserFromRoles(string username, string[] roleNames);
		void AddUserToRoles(string username, string[] roleNames);
	}
}