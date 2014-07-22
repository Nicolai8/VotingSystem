using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL.Entities;
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

		[Route("total/users", Name = "TotalUsers")]
		[HttpGet]
		public int GetTotalUsers()
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			return membership.GetTotalUsers();
		}

		[Route("total/suggestedusers", Name = "TotalSuggestedUsers", Order = 1)]
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

		[Route("{id}")]
		[HttpDelete]
		public void Delete(string id)
		{
			Membership.DeleteUser(id);
		}

		[Route("{id}/unsuggest", Name = "UnsuggestUser")]
		[HttpPost]
		public void UnsuggestUserToBlock(string id)
		{
			MembershipUser user = Membership.GetUser(id);
			if (user != null && user.ProviderUserKey != null)
			{
				UserProfile userProfile = _userProfileService.GetByUserId((int)user.ProviderUserKey);
				userProfile.SuggestedToBlock = false;
				_userProfileService.Update(userProfile);
			}
		}

		[Route("{id}/suggest", Name = "SuggestUser")]
		[HttpPost]
		public void SuggestUserToBlock(string id)
		{
			UserProfile userProfile = _userProfileService.GetByUserId(UserId);
			userProfile.SuggestedToBlock = true;
			_userProfileService.Update(userProfile);
		}
	}
}
