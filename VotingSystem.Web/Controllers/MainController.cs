using System.Web.Mvc;
using VotingSystem.Web.Helpers;

namespace VotingSystem.Web.Controllers
{
	public class MainController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Captcha(bool noisy = true)
		{
			return CaptchaHelper.GetCaptcha(noisy);
		}

		public ActionResult GetTemplate(string templateName)
		{
			return PartialView(string.Format("Templates/{0}", templateName));
		}
	}
}
