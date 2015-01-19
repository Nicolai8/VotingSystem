using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;
using VotingSystem.Web.Providers;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/user")]
	[CustomAuthorizeApi(Roles = new[] { RoleType.Admin })]
	public class UserApiController : BaseApiController
	{
		private readonly IUserProfileService _userProfileService;

		public UserApiController(IUserProfileService userProfileService)
		{
			_userProfileService = userProfileService;
		}

		[Route("total/users")]
		[HttpGet]
		public int GetTotalUsers()
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			return membership.GetTotalUsers();
		}

		[Route("total/suggestedUsers", Order = 1)]
		[HttpGet]
		public int GetTotalSuggestedUsers()
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			return membership.GetTotalSuggestedUsers();
		}

		[Route("{id}")]
		[HttpPut]
		public void Put(string id, [FromBody]UserModel user)
		{
			MembershipUser membershipUser = Membership.GetUser(user.UserName);
			if (membershipUser != null && membershipUser.IsLockedOut == user.IsBlocked)
			{
				ChangeUserRoles(id, user.Roles);
				return;
			}
			ToggleLock(id);

		}

		[Route("{id}")]
		[HttpDelete]
		public void Delete(string id)
		{
			Membership.DeleteUser(id);
		}

		[Route("{id}/unsuggest")]
		[HttpPost]
		public void UnsuggestUserToBlock(string id)
		{
			MembershipUser user = Membership.GetUser(id);
			if (user != null && user.ProviderUserKey != null)
			{
				UserProfile userProfile = _userProfileService.GetUserProfileByUserId((int)user.ProviderUserKey);
				userProfile.SuggestedToBlock = false;
				_userProfileService.UpdateUserProfile(userProfile);
			}
		}

		[Route("{id}/suggest")]
		[HttpPost]
		public void SuggestUserToBlock(string id)
		{
			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(UserId);
			userProfile.SuggestedToBlock = true;
			_userProfileService.UpdateUserProfile(userProfile);
		}

		#region Private methods

		private void ChangeUserRoles(string userName, string[] roleNames)
		{
			string[] userRoles = Roles.GetRolesForUser(userName);
			CustomeRoleProvider roleProvider = (CustomeRoleProvider)Roles.Provider;
			roleProvider.RemoveUserFromRoles(userName);
			try
			{
				Roles.AddUserToRoles(userName, roleNames);
			}
			catch (Exception)
			{
				Roles.AddUserToRoles(userName, userRoles);
				throw;
			}
		}

		private void ToggleLock(string userName)
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			membership.ToggleLockUser(userName);
		}

		[Route("{pageType}/{page:int}")]
		public IEnumerable<UserModel> Get(PageType pageType, int page = 1, int size = 10)
		{
			MembershipUserCollection membershipUsers;
			List<UserModel> users = new List<UserModel>();
			switch (pageType)
			{
				case PageType.SuggestedUsers:
					CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
					membershipUsers = membership.GetSuggestedUsers(page, size);
					break;
				default:
					int totalUsers;
					membershipUsers = Membership.GetAllUsers(page, size, out totalUsers);
					break;
			}
			foreach (MembershipUser user in membershipUsers)
			{
				users.Add(user.ToUserModel());
			}
			return users;
		}

		#endregion
	}
}
