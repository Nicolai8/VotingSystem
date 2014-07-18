using System.Security.Authentication;
using System.Web.Mvc;
using System.Web.Security;

namespace VotingSystem.Web.Controllers
{
	public class BaseController : Controller
	{
		protected int UserId
		{
			get
			{
				MembershipUser user = Membership.GetUser();
				if (user != null && user.ProviderUserKey != null)
				{
					return (int)user.ProviderUserKey;
				}

				throw new AuthenticationException();
			}
		}
	}
}
