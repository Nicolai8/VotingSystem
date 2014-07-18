using System;
using System.Configuration;
using System.Net.Configuration;
using System.Web;

namespace VotingSystem.Web.Helpers
{
	public static class GlobalVariables
	{
		public static string DefaultImagePath
		{
			get
			{
				return ConfigurationManager.AppSettings["DefaulPictureImageUrl"];
			}
		}

		public static int Captcha
		{
			get
			{
				return Convert.ToInt32(HttpContext.Current.Session["Captcha"].ToString());
			}
			set
			{
				HttpContext.Current.Session["Captcha"] = value;
			}
		}

		public static bool IsCheckedToday
		{
			get
			{
				if (HttpContext.Current.Application["CheckedDay"] == null)
				{
					HttpContext.Current.Application["CheckedDay"] = DateTime.UtcNow.AddDays(-1);
				}

				return Convert.ToDateTime(HttpContext.Current.Application["CheckedDay"]).Day == DateTime.UtcNow.Day;
			}
			set
			{
				HttpContext.Current.Application["IsCheckedToday"] = value;
			}
		}

		public static SmtpSection SmtpParameters
		{
			get
			{
				return (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			}
		}

		public static string PicturesFolder
		{
			get
			{
				return ConfigurationManager.AppSettings["PicturesFolder"];
			}
		}
	}
}