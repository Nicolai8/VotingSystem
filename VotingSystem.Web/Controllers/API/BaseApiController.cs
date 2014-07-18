using System.Security.Authentication;
using System.Web.Http;
using System.Web.Security;

namespace VotingSystem.Web.Controllers.API
{
	public class BaseApiController : ApiController
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