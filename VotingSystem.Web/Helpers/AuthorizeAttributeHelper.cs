using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Helpers
{
	public static class AuthorizeAttributeHelper
	{
		public static bool IsAuthorized(RoleType[] roles)
		{
			bool isInRole = true;
			if (roles.Length > 0)
			{
				MembershipUser membershipUser = Membership.GetUser();
				isInRole = membershipUser != null && Roles.GetRolesForUser(membershipUser.UserName)
					.Intersect(roles.Select(r => r.ToString())).Any();
			}
			return HttpContext.Current.User.Identity.IsAuthenticated && isInRole;
		}

		public static bool IsAuthorizedLog(bool isAuthorized)
		{
			if (!isAuthorized)
			{
				Logger.Warn("Unauthorized request");
			}
			return isAuthorized;
		}

		public static bool Owns<T>(string id, Func<int, T> getCommentOwner, Func<T, int> getCommentOwnerId) where T : class
		{
			int objectId;
			if (Int32.TryParse(id, out objectId))
			{
				MembershipUser user = Membership.GetUser();
				if (user == null)
				{
					return false;
				}

				string[] roles = Roles.GetRolesForUser(user.UserName);
				if ((int)user.ProviderUserKey != getCommentOwnerId(getCommentOwner(objectId))
					&& !roles.Contains(RoleType.Admin.ToString()))
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			return true;
		}

		public static int GetUserIdFromComment(Comment comment)
		{
			return comment != null && comment.UserId.HasValue ? comment.UserId.Value : -1;
		}

		public static int GetUserIdFromTheme(Theme theme)
		{
			return theme != null && theme.UserId.HasValue ? theme.UserId.Value : -1;
		}
	}
}