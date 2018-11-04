using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace HighSchool.OAuth
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "adrian10596@live.com",
                    Username = "AdrianV10596",
                    Password = "password",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Adrian Vega"),
                    },
                },
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[] {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read user data"
                }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[] {
                new Client
                {
                    ClientId = "highschool_owner_password",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "HighSchool",
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>{
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    Enabled = true

                },
                new Client
                {
                    ClientId = "highschool_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "HighSchool",
                    Flow = Flows.Implicit,
                    AllowedScopes = new List<string>{
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:56622",
                        "http://localhost:56622/",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:56622"
                    },
                    Enabled = true
                },
                new Client
                {
                    ClientId = "highschool_hybrid",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "HighSchool",
                    Flow = Flows.Hybrid,
                    AllowedScopes = new List<string>{
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:56622/",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:56622",
                    },
                    Enabled = true
                },
                new Client
                {
                    ClientId = "highschool_code",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "HighSchool",
                    Flow = Flows.Hybrid,
                    AllowedScopes = new List<string>{
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.OfflineAccess,
                        "read"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://localhost:56622",
                        "http://localhost:56622/",
                        "http://localhost:56622/Home/AuthorizationCallback"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:56622"
                    },
                    Enabled = true
                },
            };
        }
    }
}