using Management;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace HighSchool.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class ProfilesController : HighSchoolApi
    {
        private IUserManagement _userManagement { get; set; }
        private IProfileManagement _profileManagement { get; set; }

        public ProfilesController(IUserManagement userManagement, IProfileManagement profileMangament)
        {
            _userManagement = userManagement;
            _profileManagement = profileMangament;
        }

        [HttpGet]
        [Route("profiles")]
        /*It is bad practice to login or return user information*/
        public async Task<IHttpActionResult> GetProfile(string username, string password)
        {
            try
            {
                var user = await _userManagement.GetUserAsync(password, username);

                if (user == null)
                    return NotFound();

                var profile = await _profileManagement.GetProfileByIdUserAsync(user.IdUser);

                if (profile == null)
                    return NotFound();

                return Ok(profile);
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [ScopeAuthorize("read")]
        [Route("profiles")]
        public async Task<IHttpActionResult> GetProfile()
        {
            try
            {
                var username = GetUsernameFromClaim();

                var user = await _userManagement.GetUserByUsernameAsync(username);

                if (user == null)
                    return NotFound();

                var profile = await _profileManagement.GetProfileByIdUserAsync(user.IdUser);

                if (profile == null)
                    return NotFound();

                return Ok(profile);
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }
    }
}
