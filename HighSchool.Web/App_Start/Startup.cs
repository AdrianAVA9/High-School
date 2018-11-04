using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

[assembly: OwinStartup(typeof(HighSchool.Web.App_Start.Startup))]

namespace HighSchool.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap
                = new Dictionary<string, string>();


            /*----------------------------USING OWNER PASSWORD--------------------------------------*/

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login")
            });




            /*----------------------------USING HYBRID FLOW--------------------------------------*/

            ////app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions()
            //{
            //    ClientId = "highschool_hybrid",
            //    Authority = "http://localhost:49875",
            //    RedirectUri = "http://localhost:56622/",
            //    ResponseType = "code id_token",
            //    Scope = "openid profile read offline_access",
            //    RequireHttpsMetadata = false,
            //    PostLogoutRedirectUri = "http://localhost:56622",
            //    SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

            //    Notifications = new OpenIdConnectAuthenticationNotifications()
            //    {
            //        AuthorizationCodeReceived = async notification =>
            //       {
            //           var requestResponse = await OidcClient.CallTokenEndpointAsync(
            //               new Uri("http://localhost:49875/connect/token"),
            //               new Uri("http://localhost:56622/"),
            //               notification.Code,
            //               "highschool_hybrid",
            //               "secret"
            //               );

            //           var identity = notification.AuthenticationTicket.Identity;

            //           identity.AddClaim(new Claim("access_token", requestResponse.AccessToken));
            //           identity.AddClaim(new Claim("id_token", requestResponse.IdentityToken));
            //           identity.AddClaim(new Claim("refresh_token", requestResponse.RefreshToken));

            //           notification.AuthenticationTicket = new Microsoft.Owin.Security.AuthenticationTicket
            //           (
            //               identity,
            //               notification.AuthenticationTicket.Properties
            //           );
            //       },

            //        RedirectToIdentityProvider = notification =>
            //        {
            //            if (notification.ProtocolMessage.RequestType !=
            //            Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectRequestType.Logout)
            //            {
            //                return Task.FromResult(0);
            //            }

            //            notification.ProtocolMessage.IdTokenHint
            //            = notification.OwinContext.Authentication.User.FindFirst("id_token").Value;

            //            return Task.FromResult(0);
            //        }
            //    },
            //});





            /*--------------------------USING IMPLICIT FLOW-----------------------------------*/
            /*
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions()
            {
                ClientId = "highschool_implicit",
                Authority = "http://localhost:49875",
                RedirectUri = "http://localhost:56622",
                ResponseType = "token id_token",
                Scope = "openid profile read",
                RequireHttpsMetadata = false,
                PostLogoutRedirectUri = "http://localhost:56622",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = notification =>
                    {
                        var identity = notification.AuthenticationTicket.Identity;

                        identity.AddClaim(new Claim("id_token",
                            notification.ProtocolMessage.IdToken));

                        identity.AddClaim(new Claim("token",
                            notification.ProtocolMessage.AccessToken));

                        notification.AuthenticationTicket = new Microsoft.Owin.Security.AuthenticationTicket(identity,
                        notification.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    },

                    RedirectToIdentityProvider = notification =>
                    {
                        if (notification.ProtocolMessage.RequestType !=
                        Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectRequestType.Logout)
                        {
                            return Task.FromResult(0);
                        }

                        notification.ProtocolMessage.IdTokenHint
                        = notification.OwinContext.Authentication.User.FindFirst("id_token").Value;

                        return Task.FromResult(0);
                    }
                },
            });
        **/
        }
    }
}
