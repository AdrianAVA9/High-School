using HighSchool.Web.Dtos;
using HighSchool.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Clients;

namespace HighSchool.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RefreshAccessToken()
        {
            var claimsPrincipal = User as ClaimsPrincipal;

            var client = new OAuth2Client(new Uri("http://localhost:49875/connect/token"),
                "highschool_hybrid", "secret");

            var requestResponse = client.RequestAccessTokenRefreshToken(
                claimsPrincipal.FindFirst("refresh_token").Value);

            var manager = HttpContext.GetOwinContext().Authentication;

            var refreshedIdentity = new ClaimsIdentity(User.Identity);

            refreshedIdentity.RemoveClaim(refreshedIdentity.FindFirst("access_token"));
            refreshedIdentity.RemoveClaim(refreshedIdentity.FindFirst("refresh_token"));

            refreshedIdentity.AddClaim(new Claim("access_token",
                requestResponse.AccessToken));

            refreshedIdentity.AddClaim(new Claim("refresh_token",
                requestResponse.RefreshToken));

            manager.AuthenticationResponseGrant = new Microsoft.Owin.Security.AuthenticationResponseGrant(
                new ClaimsPrincipal(refreshedIdentity),
                new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true }
                );

            return Redirect("/HomePage");
        }

        public ActionResult Login()
        {
            return View(new UserForViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserForViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel.ErrorMessage = "The username or password aren´t enough long";

                    return View(viewModel);
                }

                var client = new OAuth2Client(new Uri("http://localhost:49875/connect/token"),
                    "highschool_owner_password", "secret");

                var requestResponse = client.RequestAccessTokenUserName(viewModel.Username, viewModel.Password,
                    "openid profile offline_access read");

                var claims = new[]
                {
                    new Claim("access_token",requestResponse.AccessToken),
                    new Claim("refresh_token",requestResponse.RefreshToken),
                };

                var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(claimsIdentity);

                return Redirect("/HomePage");
            }
            catch (HttpRequestException ex)
            {
                viewModel.ErrorMessage = "The username or password are incorrect";

                return View(viewModel);
            }
            catch (Exception)
            {
                return Content("We have a error trying to connect with the sever, please try later again");
            }
        }

        [Authorize]
        public ActionResult HomePage()
        {
            var claimsPrincipals = User as ClaimsPrincipal;
            var claims = claimsPrincipals.Claims;

            if (claims == null)
                claims = new List<Claim>();

            return View(claims);

            /*
            
            This line of code call to api

             return Redirect("/Home/HomePageCallToApi");
             
             */
        }


        [Authorize]
        public async Task<ActionResult> HomePageCallToApi()
        {
            var claims = User as ClaimsPrincipal;
            ProfileDto profile = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", claims.FindFirst("token")?.Value ?? "");

                var response = await httpClient.GetAsync("http://localhost:53866/api/profiles");

                if (response.IsSuccessStatusCode)
                {
                    var profileString = await response.Content.ReadAsStringAsync();

                    profile = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileDto>(profileString);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return Content(System.Net.HttpStatusCode.Unauthorized.ToString());
                    }
                }
            }

            return View(profile);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }




        /*
         ------------------------USING CODE FLOW---------------------------
         */
        //public ActionResult Index()
        //{

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var claimsPrincipal = User as ClaimsPrincipal;
        //        return Content(claimsPrincipal.FindFirst("access_token").Value);
        //    }
        //    var url = "http://localhost:49875/connect/authorize" +
        //        "?client_id=highschool_code" +
        //        "&redirect_uri=http://localhost:56622/Home/AuthorizationCallback" +
        //        "&response_type=code" +
        //        "&scope=openid+profile" +
        //        "&response_mode=form_post";

        //    return Redirect(url);
        //}

        //public ActionResult AuthorizationCallback(string code, string state, string error)
        //{
        //    var tokenUrl = "http://localhost:49875/connect/token";

        //    var client = new OAuth2Client(new Uri(tokenUrl), "highschool_code",
        //        "secret");

        //    var requestResult = client.RequestAccessTokenCode(code,
        //        new Uri("http://localhost:56622/Home/AuthorizationCallback"));

        //    var claims = new[]
        //    {
        //        new Claim("access_token", requestResult.AccessToken)
        //    };

        //    var identity = new ClaimsIdentity(claims,
        //        DefaultAuthenticationTypes.ApplicationCookie);

        //    Request.GetOwinContext().Authentication.SignIn(identity);

        //    return Redirect("/");
        //}
    }
}