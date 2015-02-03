using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VotingSystem.BLL.Interfaces;
using VotingSystem.Common;
using VotingSystem.DAL.Entities;
using VotingSystem.DAL.Enums;
using VotingSystem.Web.Enums;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Providers;
using VotingSystem.Web.Resources;

namespace VotingSystem.Web.Controllers
{
	[CustomAuthorizeMvc]
	public class AccountController : BaseController
	{
		private readonly IUserProfileService _userProfileService;

		public AccountController(IUserProfileService userProfileService)
		{
			_userProfileService = userProfileService;
		}

		[HttpPost]
		[AllowAnonymous]
		public JsonResult Login(string userName, string password, bool rememberMe)
		{
			if (Membership.ValidateUser(userName, password))
			{
				FormsAuthentication.SetAuthCookie(userName, rememberMe);
				return Json(new { result = true }, JsonRequestBehavior.AllowGet);
			}
			throw new VotingSystemException(Errors.IncorrectUsernameOrPassword);
		}

		[HttpPost]
		public JsonResult LogOff()
		{
			FormsAuthentication.SignOut();
			return Json(new { result = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		[AllowAnonymous]
		public JsonResult Register(string newUserName, string email)
		{
			try
			{
				MembershipCreateStatus status;
				string newPassword = Membership.GeneratePassword(10, 1);
				Membership.CreateUser(newUserName, newPassword, email, "Temp", "Temp", false, null, out status);
				if (status == MembershipCreateStatus.Success)
				{
					Roles.AddUserToRole(newUserName, "User");
					MailHelper.SendEmail(newUserName, newPassword,
						Resource.RegistrationMailSubject,
						Resource.RegistrationMailBody,
						email,
						Request.UrlReferrer.AbsoluteUri);
					return Json(new { result = true }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (SmtpException)
			{
				Membership.DeleteUser(newUserName);
			}
			throw new VotingSystemException(Errors.RegistrationFailed);
		}

		[HttpPost]
		public void ChangePassword(string oldPassword, string newPassword)
		{
			MembershipUser user = Membership.GetUser();
			if (user != null && user.ChangePassword(oldPassword, newPassword))
			{
				return;
			}
			throw new VotingSystemException(Errors.UpdatePasswordFailed);
		}

		[HttpGet]
		public string IsInRole()
		{
			CustomRoleProvider roleProvider = (CustomRoleProvider)Roles.Provider;
			List<string> roles = roleProvider.GetRolesForUser(UserId).ToList();
			return string.Join(",", roles);
		}

		[HttpGet]
		[AllowAnonymous]
		public JsonResult UserNameExists(string userName)
		{
			bool exists = Membership.GetUser(userName) != null;
			return Json(new { result = exists }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public string UploadPicture(HttpPostedFileBase picture)
		{
			picture = picture ?? Request.Files["picture"];
			if (picture == null)
			{
				throw new ArgumentException(Errors.PictureNotFound);
			}

			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(UserId);

			string oldPicture = userProfile.PictureUrl;
			FileHelper.DeletePicture(Server, oldPicture);

			string pictureUrl = FileHelper.SavePicture(Server, picture);
			userProfile.PictureUrl = pictureUrl;

			_userProfileService.UpdateUserProfile(userProfile);
			return pictureUrl;
		}

		[HttpPost]
		public void ChangePrivacy(PrivacyType privacy)
		{
			UserProfile userProfile = _userProfileService.GetUserProfileByUserId(UserId);
			userProfile.Privacy = privacy;
			_userProfileService.UpdateUserProfile(userProfile);
		}

		[CustomAuthorizeMvc(Roles = new[] { RoleType.Admin })]
		public JsonResult GetAllRoles()
		{
			return Json(Roles.GetAllRoles(), JsonRequestBehavior.AllowGet);
		}
	}
}
