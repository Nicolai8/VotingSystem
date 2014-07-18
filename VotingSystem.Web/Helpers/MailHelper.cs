using System;
using System.Net.Mail;

namespace VotingSystem.Web.Helpers
{
	public static class MailHelper
	{
		public static void SendEmail(string userName, string password, string subject, string bodyTemplate, string email, string url)
		{
			using (MailMessage mail = new MailMessage(GlobalVariables.SmtpParameters.From, email))
			{
				mail.Subject = subject;
				mail.Body = String.Format(bodyTemplate, userName, password, url);
				mail.IsBodyHtml = true;
				using (SmtpClient client = new SmtpClient())
				{
					client.Send(mail);
				}
			}
		}
	}
}
