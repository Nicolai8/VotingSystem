using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;

namespace VotingSystem.Web.Providers
{
	public class CustomRoleProvider : RoleProvider
	{
		private IRoleService _roleService
		{
			get
			{
				return DependencyResolver.Current.GetService<IRoleService>();
			}
		}

		private IUserService _userService
		{
			get
			{
				return DependencyResolver.Current.GetService<IUserService>();
			}
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return _userService.IsUserInRole(username, roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			return _userService.GetUserRolesByUserName(username);
		}

		public string[] GetRolesForUser(int userId)
		{
			return _userService.GetUserRolesByUserId(userId);
		}

		public override void CreateRole(string roleName)
		{
			if (String.IsNullOrEmpty(roleName))
			{
				throw new ProviderException("Role name cannot be empty or null.");
			}
			if (roleName.Contains(","))
			{
				throw new ProviderException("Role names cannot contain commas.");
			}
			if (RoleExists(roleName))
			{
				throw new ProviderException("Role name already exists.");
			}
			try
			{
				_roleService.CreateRole(roleName);
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			try
			{
				_roleService.DeleteRole(roleName);
				return true;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override bool RoleExists(string roleName)
		{
			return _roleService.GetRole(roleName) != null;
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			foreach (string username in usernames)
			{
				_userService.AddUserToRoles(username, roleNames);
			}
		}

		public void AddUserToRoles(int userId, string[] roleNames)
		{
			_userService.AddUserToRoles(userId, roleNames);
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			foreach (string username in usernames)
			{
				_userService.RemoveUserFromRoles(username, roleNames);
			}
		}

		public override string[] GetUsersInRole(string roleName)
		{
			return _roleService.GetRole(roleName).Users.Select(u => u.UserName).ToArray();
		}

		public override string[] GetAllRoles()
		{
			return _roleService.GetAllRoles().Select(r => r.RoleName).ToArray();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			return GetUsersInRole(roleName).Where(userName => usernameToMatch.Contains(usernameToMatch)).ToArray();
		}

		public override string ApplicationName { get; set; }

		public void RemoveUserFromRoles(int userId)
		{
			_userService.RemoveUserFromAllRoles(userId);
		}
	}
}