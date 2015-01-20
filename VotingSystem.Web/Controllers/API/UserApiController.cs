using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;
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

		[HttpGet]
		[Route("total/users")]
		public int GetTotalUsers()
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			return membership.GetTotalUsers();
		}

		[HttpGet]
		[Route("total/suggestedUsers", Order = 1)]
		public int GetTotalSuggestedUsers()
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			return membership.GetTotalSuggestedUsers();
		}

		[HttpPut]
		[Route("{id}")]
		public void Put(int id, [FromBody]UserModel user)
		{
			MembershipUser membershipUser = Membership.GetUser(id);
			if (membershipUser != null && membershipUser.IsLockedOut == user.IsBlocked)
			{
				ChangeUserRoles(id, user.Roles);
				return;
			}
			ToggleLock(id);
		}

		[HttpDelete]
		[Route("{id}")]
		public void Delete(int id)
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			membership.DeleteUser(id);
		}

		[HttpPost]
		[Route("{id}/unsuggest")]
		public void UnsuggestUserToBlock(int id)
		{
			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(id);
			userProfile.SuggestedToBlock = false;
			_userProfileService.UpdateUserProfile(userProfile);
		}

		[HttpPost]
		[Route("{id}/suggest")]
		public void SuggestUserToBlock(int id)
		{
			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(id);
			userProfile.SuggestedToBlock = true;
			_userProfileService.UpdateUserProfile(userProfile);
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

		[HttpGet]
		[Route("profile/{id?}")]
		[AllowAnonymous]
		public UserModel UserProfile(int? id = null)
		{
			UserModel user = new UserModel();
			MembershipUser membershipUser = id.HasValue ? Membership.GetUser(id) : Membership.GetUser();
			if (membershipUser != null)
			{
				user = membershipUser.ToUserModel(_userProfileService.GetUserProfileByUserId((int)membershipUser.ProviderUserKey));
				if (!id.HasValue)
				{
					user.Privacy = PrivacyType.WholeWorld;
				}
				HideFieldsAccordingToPrivacy(user);
			}
			return user;
		}

		#region Private methods

		private void ChangeUserRoles(int userId, string[] roleNames)
		{
			CustomRoleProvider roleProvider = (CustomRoleProvider)Roles.Provider;
			string[] userRoles = roleProvider.GetRolesForUser(userId);
			roleProvider.RemoveUserFromRoles(userId);
			try
			{
				roleProvider.AddUserToRoles(userId, roleNames);
			}
			catch (Exception)
			{
				roleProvider.AddUserToRoles(userId, userRoles);
				throw;
			}
		}

		private void ToggleLock(int userId)
		{
			CustomMembershipProvider membership = (CustomMembershipProvider)Membership.Provider;
			membership.ToggleLockUser(userId);
		}

		private void HideFieldsAccordingToPrivacy(UserModel user)
		{
			switch (user.Privacy)
			{
				case PrivacyType.Users:
					if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
					{
						user.Privacy = null;
						user.Email = "Hidden";
					}
					break;
				case PrivacyType.WholeWorld:
					break;
				default:
					user.Privacy = null;
					user.Email = "Hidden";
					break;
			}
		}

		#endregion
	}
}
