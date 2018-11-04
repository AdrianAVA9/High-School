using System.Security.Claims;
using System.Web.Http;

namespace HighSchool.WebApi.Controllers
{
    public class HighSchoolApi : ApiController
    {
        protected string GetUsernameFromClaim()
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}