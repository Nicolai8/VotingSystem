using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class UserService : BaseService, IUserService
	{
		private readonly IRoleService _roleService;

		public UserService(IUnitOfWork unitOfWork, IRoleService roleService)
			: base(unitOfWork)
		{
			_roleService = roleService;
		}

		public User GetUser(string userName)
		{
			return UnitOfWork.UserRepository.Query()
				.Filter(u => u.UserName.Equals(userName))
				.Include(u => u.Roles)
				.Get().SingleOrDefault();
		}

		public List<User> GetUsers(out int total, int page = 1, int pageSize = 10)
		{
			total = UnitOfWork.UserRepository.GetTotal();
			return UnitOfWork.UserRepository.Query()
				.OrderBy(u => u.OrderBy(us => us.UserName))
				.GetPage(page, pageSize).ToList();
		}

		public List<User> GetSuggestedUsers(int page = 1, int pageSize = 10)
		{
			return UnitOfWork.UserRepository.Query()
				.Filter(u => u.UserProfile.SuggestedToBlock)
				.OrderBy(u => u.OrderBy(us => us.UserName))
				.GetPage(page, pageSize).ToList();
		}

		public User GetUserByEmail(string email)
		{
			return UnitOfWork.UserRepository.Query()
				.Filter(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
				.Get().SingleOrDefault();
		}

		public User GetUserById(int id)
		{
			return UnitOfWork.UserRepository.GetById(id);
		}

		public string[] GetUserRoles(string userName)
		{
			return GetUser(userName).Roles.Select(r => r.RoleName).ToArray();
		}

		public List<User> GetUsersByName(string userName)
		{
			return UnitOfWork.UserRepository.Query()
				.Filter(u => u.UserName.Equals(userName))
				.OrderBy(u => u.OrderBy(us => us.UserName))
				.Get().ToList();
		}

		public void UpdateUser(User user)
		{
			User currentUser = GetUser(user.UserName);
			currentUser.Roles = user.Roles;
			UnitOfWork.Save();
		}

		public void UpdateUser(string userName, string email = null, bool? isApproved = null, string password = null, string passwordQuestion = null, string passwordAnswer = null)
		{
			User currentUser = GetUser(userName);

			currentUser.Email = SetValueIfNotNull(currentUser.Email, email);
			currentUser.Password = SetValueIfNotNull(currentUser.Password, password);
			currentUser.IsApproved = Convert.ToBoolean(SetValueIfNotNull(currentUser.IsApproved, isApproved));
			currentUser.PasswordQuestion = SetValueIfNotNull(currentUser.PasswordQuestion, passwordQuestion);
			currentUser.PasswordAnswer = SetValueIfNotNull(currentUser.PasswordAnswer, passwordAnswer);

			UnitOfWork.Save();
		}

		public void DeleteUser(string userName)
		{
			UnitOfWork.UserRepository.Delete(GetUser(userName).Id);
			UnitOfWork.Save();
		}

		public void CreateUser(User user)
		{
			UnitOfWork.UserRepository.Insert(user);
			UnitOfWork.Save();
		}

		public bool IsUserInRole(string userName, string roleName)
		{
			User user = GetUser(userName);
			return user != null && user.Roles.Any(r => r.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
		}

		public bool ToggleLock(string userName)
		{
			User user = GetUser(userName);
			user.IsLocked = !user.IsLocked;
			UnitOfWork.Save();
			return user.IsLocked;
		}

		public int GetTotal(Expression<Func<User, bool>> filter = null)
		{
			return UnitOfWork.UserRepository.GetTotal(filter);
		}

		public void RemoveUserFromAllRoles(string username)
		{
			User user = GetUser(username);
			user.Roles = new Collection<Role>();
			UnitOfWork.Save();
		}

		public void RemoveUserFromRoles(string username, string[] roleNames)
		{
			List<Role> roles = roleNames.Select(roleName => _roleService.GetRole(roleName)).ToList();
			User user = GetUser(username);
			if (user.Roles != null)
			{
				user.Roles = user.Roles.Except(roles).ToList();
			}
			UnitOfWork.Save();
		}

		public void AddUserToRoles(string username, string[] roleNames)
		{
			List<Role> roles = roleNames.Select(roleName => _roleService.GetRole(roleName)).ToList();
			User user = GetUser(username);
			if (user.Roles != null)
			{
				roles.AddRange(user.Roles);
				roles = roles.Distinct().ToList();
			}
			user.Roles = new List<Role>();
			roles.ForEach(user.Roles.Add);
			UnitOfWork.Save();
		}
		
		private T SetValueIfNotNull<T>(T currentValue, T newValue)
		{
			return newValue == null ? currentValue : newValue;
		}
	}
}
