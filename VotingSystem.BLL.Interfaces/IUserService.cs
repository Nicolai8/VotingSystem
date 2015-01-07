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
		//REVIEW: Need to find one way in service methods naming. There method called UpdateUser in ICommentService we have Insert method.
		void UpdateUser(User user);
		void UpdateUser(string userName, string email = null, bool? isApproved = null, string password = null,
			string passwordQuestion = null, string passwordAnswer = null);
		//REVIEW: deleteAllRelatedData parameter never used
		void DeleteUser(string userName, bool deleteAllRelatedData);
		void CreateUser(User user);
		bool IsUserInRole(string userName, string roleName);
		bool ToggleLock(string userName);
		int GetTotal(Expression<Func<User, bool>> filter = null);
		//REVIEW: could be renamed to RemoveUserFromAllRoles
		//btw, interesting we add or remove role from user or user from roles? :)
		void RemoveUserFromRoles(string username);
		void RemoveUserFromRoles(string username, string[] roleNames);
		void AddUserToRoles(string username, string[] roleNames);
	}
}