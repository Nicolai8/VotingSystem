﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IUserService
	{
		User GetUserByUserName(string userName);

		List<User> GetUsersByName(string userName);

		List<User> GetUsers(out int total, Filter filter);

		List<User> GetSuggestedUsers(Filter filter);

		string[] GetUserRoles(string userName);

		User GetUserByEmail(string email);

		User GetUserById(int id);

		bool ToggleLock(string userName);

		void UpdateUser(User user);

		void UpdateUser(string userName, string email = null, bool? isApproved = null, string password = null,
			string passwordQuestion = null, string passwordAnswer = null);

		void AddUserToRoles(string username, string[] roleNames);

		void RemoveUserFromAllRoles(string username);

		void RemoveUserFromRoles(string username, string[] roleNames);

		void DeleteUser(string userName);

		void CreateUser(User user);

		bool IsUserInRole(string userName, string roleName);

		int GetNumberOfUsers(Expression<Func<User, bool>> filter = null);
	}
}