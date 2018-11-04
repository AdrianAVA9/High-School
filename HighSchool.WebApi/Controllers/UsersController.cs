using Management;
using System.Threading.Tasks;
using System.Web.Http;

namespace HighSchool.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UsersController : HighSchoolApi
    {
        private IUserManagement _userManagement { get; set; }
        private IProfileManagement _profileManagement { get; set; }

        public UsersController(IUserManagement userManagement, IProfileManagement profileMangament)
        {
            _userManagement = userManagement;
            _profileManagement = profileMangament;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IHttpActionResult> GetUsers()
        {
            try
            {
                var users = await _userManagement.GetUsersAsync();

                return Ok(users);
            }
            catch (System.Exception)
            {
                return InternalServerError();
            }
        }
    }
}
