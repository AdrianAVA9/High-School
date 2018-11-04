using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Management;
using Microsoft.Owin;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(HighSchool.OAuth.Startup))]

namespace HighSchool.OAuth
{
    public partial class Startup
    {
        public class HighSchoolUserServices : UserServiceBase
        {
            public IUserManagement _userManager { get; set; }

            public HighSchoolUserServices(IUserManagement userManagement)
            {
                _userManager = userManagement;
            }

            public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
            {
                var user = await _userManager.GetUserAsync(context.Password, context.UserName);

                if (user == null)
                {
                    context.AuthenticateResult = new AuthenticateResult("Incorrect credentials");
                    return;
                }

                context.AuthenticateResult = new AuthenticateResult("/Terms", context.UserName, user.Username);
            }
        }
    }
}
