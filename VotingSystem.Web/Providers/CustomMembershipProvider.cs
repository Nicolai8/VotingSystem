using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Mvc;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Structures;

namespace VotingSystem.Web.Providers
{
	public sealed class CustomMembershipProvider : MembershipProvider
	{
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (name.Length == 0)
			{
				name = "CustomMembershipProvider";
			}
			if (String.IsNullOrEmpty(config["description"]))
			{
				config["description"] = "Custom Membership Provider";
			}
			base.Initialize(name, config);

			_applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
			_maxInvalidPasswordAttempts = GetConfigValue(config["maxInvalidPasswordAttempts"], 5);
			_passwordAttemptWindow = GetConfigValue(config["passwordAttemptWindow"], 10);
			_minRequiredNonAlphanumericCharacters = GetConfigValue(config["minRequiredNonAlphanumericCharacters"], 1);
			_minRequiredPasswordLength = GetConfigValue(config["minRequiredPasswordLength"], 7);
			_passwordStrengthRegularExpression = GetConfigValue(config["passwordStrengthRegularExpression"], "");
			_enablePasswordReset = GetConfigValue(config["enablePasswordReset"], true);
			_requiresQuestionAndAnswer = GetConfigValue(config["requiresQuestionAndAnswer"], false);
			_requiresUniqueEmail = GetConfigValue(config["requiresUniqueEmail"], true);

			string tempFormat = config["passwordFormat"] ?? "hashed";
			switch (tempFormat)
			{
				case "hashed":
					_passwordFormat = MembershipPasswordFormat.Hashed;
					break;
				case "clear":
					_passwordFormat = MembershipPasswordFormat.Clear;
					break;
				default:
					throw new ProviderException("Password format not supported.");
			}
		}

		#region Properties

		private const int NewPasswordLength = 8;

		private IUserService _userService
		{
			get
			{
				return DependencyResolver.Current.GetService<IUserService>();
			}
		}

		private string _applicationName;
		private bool _enablePasswordReset;
		private bool _requiresQuestionAndAnswer;
		private bool _requiresUniqueEmail;
		private int _maxInvalidPasswordAttempts;
		private int _passwordAttemptWindow;
		private MembershipPasswordFormat _passwordFormat;
		private int _minRequiredNonAlphanumericCharacters;
		private int _minRequiredPasswordLength;
		private string _passwordStrengthRegularExpression;

		public override string ApplicationName
		{
			get
			{
				return _applicationName;
			}
			set
			{
				_applicationName = value;
			}
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordReset
		{
			get
			{
				return _enablePasswordReset;
			}
		}

		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				return _requiresQuestionAndAnswer;
			}
		}

		public override bool RequiresUniqueEmail
		{
			get
			{
				return _requiresUniqueEmail;
			}
		}

		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				return _maxInvalidPasswordAttempts;
			}
		}

		public override int PasswordAttemptWindow
		{
			get
			{
				return _passwordAttemptWindow;
			}
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return _passwordFormat;
			}
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return _minRequiredNonAlphanumericCharacters;
			}
		}

		public override int MinRequiredPasswordLength
		{
			get
			{
				return _minRequiredPasswordLength;
			}
		}

		public override string PasswordStrengthRegularExpression
		{
			get
			{
				return _passwordStrengthRegularExpression;
			}
		}
		#endregion

		#region MembershipMethods

		public override bool ChangePassword(string username, string oldPwd, string newPwd)
		{
			if (!ValidateUser(username, oldPwd))
			{
				return false;
			}

			ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPwd, true);
			OnValidatingPassword(args);
			if (args.Cancel)
			{
				if (args.FailureInformation != null)
				{
					throw args.FailureInformation;
				}
				throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
			}

			try
			{
				string newPassword = EncodePassword(newPwd);
				_userService.UpdateUser(username, password: newPassword);

				return true;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer)
		{
			if (!ValidateUser(username, password))
			{
				return false;
			}
			try
			{
				_userService.UpdateUser(username, passwordQuestion: newPwdQuestion, passwordAnswer: newPwdAnswer);
				return true;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
			bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
			OnValidatingPassword(args);
			if (args.Cancel)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
			{
				status = MembershipCreateStatus.DuplicateEmail;
				return null;
			}
			MembershipUser u = GetUser(username, false);
			if (u != null)
			{
				status = MembershipCreateStatus.DuplicateUserName;
				return null;
			}
			DateTime createDate = DateTime.Now;

			User newUser = new User
				{
					UserName = username,
					Password = EncodePassword(password),
					Email = email,
					PasswordQuestion = passwordQuestion,
					PasswordAnswer = String.IsNullOrEmpty(passwordAnswer) ? "" : EncodePassword(passwordAnswer),
					CreateDate = createDate,
					IsLocked = false,
					ApproveSendDate = createDate.AddDays(-1),
					UserProfile = new UserProfile { Privacy = PrivacyType.Closed },
				};

			try
			{
				_userService.CreateUser(newUser);
				status = MembershipCreateStatus.Success;
			}
			catch (Exception)
			{
				status = MembershipCreateStatus.ProviderError;
			}
			return GetUser(username, false);
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			try
			{
				_userService.DeleteUser(username);
				return true;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			MembershipUserCollection users = new MembershipUserCollection();
			totalRecords = 0;
			try
			{
				List<User> usersInDb = _userService.GetUsers(out totalRecords, pageIndex, pageSize);
				foreach (User user in usersInDb)
				{
					users.Add(ConvertUsertToMembershipUser(user));
				}
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
			return users;
		}

		public MembershipUserCollection GetSuggestedUsers(int pageIndex, int pageSize)
		{
			MembershipUserCollection users = new MembershipUserCollection();
			try
			{
				List<User> usersInDb = _userService.GetSuggestedUsers(pageIndex, pageSize);
				foreach (User user in usersInDb)
				{
					users.Add(ConvertUsertToMembershipUser(user));
				}
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
			return users;
		}

		public override int GetNumberOfUsersOnline()
		{
			return 0;
		}

		public override string GetPassword(string username, string answer)
		{
			throw new ProviderException("Password Retrieval Not Enabled.");
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			try
			{
				return ConvertUsertToMembershipUser(_userService.GetUser(username));
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			try
			{
				return ConvertUsertToMembershipUser(_userService.GetUserById((int)providerUserKey));
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override bool UnlockUser(string username)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			try
			{
				User user = _userService.GetUserByEmail(email);
				return user != null ? user.UserName : "";
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override string ResetPassword(string username, string answer)
		{
			if (!EnablePasswordReset)
			{
				throw new NotSupportedException("Password reset is not enabled.");
			}
			if (answer == null && RequiresQuestionAndAnswer)
			{
				UpdateFailureCount(username, "passwordAnswer");
				throw new ProviderException("Password answer required for password reset.");
			}
			string newPassword = Membership.GeneratePassword(NewPasswordLength, MinRequiredNonAlphanumericCharacters);
			ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
			OnValidatingPassword(args);
			if (args.Cancel)
			{
				if (args.FailureInformation != null)
				{
					throw args.FailureInformation;
				}
				throw new MembershipPasswordException("Reset password canceled due to password validation failure.");
			}
			try
			{
				User user = _userService.GetUser(username);
				string passwordAnswer;
				if (user != null)
				{
					if (user.IsLocked)
						throw new MembershipPasswordException("The supplied user is locked out.");
					passwordAnswer = user.PasswordAnswer;
				}
				else
				{
					throw new MembershipPasswordException("The supplied user name is not found.");
				}
				if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
				{
					//UpdateFailureCount(username, "passwordAnswer");
					throw new MembershipPasswordException("Incorrect password answer.");
				}
				_userService.UpdateUser(username, password: newPassword);
				return newPassword;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override void UpdateUser(MembershipUser user)
		{
			try
			{
				User userInDb = _userService.GetUser(user.UserName);
				userInDb.Email = user.Email;
				userInDb.IsApproved = user.IsApproved;
				_userService.UpdateUser(user.UserName, user.Email, user.IsApproved);
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override bool ValidateUser(string username, string password)
		{
			try
			{
				User user = _userService.GetUser(username);
				if (user != null && !user.IsLocked)
				{
					if (CheckPassword(password, user.Password))
					{
						if (!user.IsApproved)
						{
							_userService.UpdateUser(username, isApproved: true);
						}
						return user.IsApproved && !user.IsLocked;
					}
					//else
					//{
					//	UpdateFailureCount(username, "password");
					//}
				}
				return false;

			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			MembershipUserCollection users = new MembershipUserCollection();
			totalRecords = 0;
			try
			{
				List<User> usersInDb = _userService.GetUsersByName(usernameToMatch);
				foreach (User user in usersInDb)
				{
					users.Add(ConvertUsertToMembershipUser(user));
				}
				return users;
			}
			catch (Exception e)
			{
				throw new ProviderException(e.Message);
			}
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Toggle user lock field
		/// </summary>
		/// <param name="userName">username of user</param>
		/// <returns>New value of lock field</returns>
		public bool ToggleLockUser(string userName)
		{
			return _userService.ToggleLock(userName);
		}

		public int GetTotalUsers()
		{
			return _userService.GetTotal();
		}

		public int GetTotalSuggestedUsers()
		{
			return _userService.GetTotal(u => u.UserProfile.SuggestedToBlock);
		}
		#endregion

		#region Helpers

		private T GetConfigValue<T>(string configValue, T defaultValue)
		{
			if (String.IsNullOrEmpty(configValue))
			{
				configValue = defaultValue.ToString();
			}
			return (T)Convert.ChangeType(configValue, typeof(T));
		}

		//REVIEW: Remove unused methods
		private void UpdateFailureCount(string username, string failureType)
		{
			throw new NotImplementedException();
		}

		private bool CheckPassword(string password, string dbpassword)
		{
			switch (PasswordFormat)
			{
				case MembershipPasswordFormat.Hashed:
					return SecurityHelper.ComparePasswords(password, dbpassword);
			}
			return password == dbpassword;
		}

		private string EncodePassword(string password)
		{
			string encodedPassword = password;
			switch (PasswordFormat)
			{
				case MembershipPasswordFormat.Clear:
					break;
				case MembershipPasswordFormat.Hashed:
					encodedPassword = SecurityHelper.CreateHash(password);
					break;
				default:
					throw new ProviderException("Unsupported password format.");
			}
			return encodedPassword;
		}

		private MembershipUser ConvertUsertToMembershipUser(User user)
		{
			return user == null ? null : new MembershipUser(
						Name, user.UserName,
						user.Id,
						user.Email,
						user.PasswordQuestion,
						string.Empty,
						user.IsApproved,
						user.IsLocked,
						user.CreateDate,
						DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
		}
		#endregion
	}
}