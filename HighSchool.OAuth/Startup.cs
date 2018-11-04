using HighSchool.Data.Repositories;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.EntityFramework;
using Management;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

[assembly: OwinStartup(typeof(HighSchool.OAuth.Startup))]

namespace HighSchool.OAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var entityFrameworkOptions = new EntityFrameworkServiceOptions
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["HighSchool.IdSvr"].ConnectionString,
            };

            var inMemoryManager = new InMemoryManager();
            SetUpClients(inMemoryManager.GetClients(), entityFrameworkOptions);
            SetUpScopes(inMemoryManager.GetScopes(), entityFrameworkOptions);

            var userManagement = new UserManagement(
                new UnitOfWork(ConfigurationManager.ConnectionStrings["HighSchool"].ConnectionString));

            new TokenCleanup(entityFrameworkOptions, 1).Start();

            var factory = new IdentityServerServiceFactory();
            factory.RegisterConfigurationServices(entityFrameworkOptions);
            factory.RegisterOperationalServices(entityFrameworkOptions);
            factory.UserService = new Registration<IdentityServer3.Core.Services.IUserService>(
                    typeof(HighSchoolUserServices));
            factory.Register(new Registration<IUserManagement>(userManagement));


            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SignedCertificate"]);

            var options = new IdentityServerOptions
            {
                SiteName = "HighSchool",
                SigningCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certificate,
                ConfigurationManager.AppSettings["SingedCertificatePassword"]),
                RequireSsl = false,
                Factory = factory,

            };
            app.UseIdentityServer(options);
        }

        public void SetUpClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var context = new ClientConfigurationDbContext(
                options.ConnectionString, options.Schema))
            {
                if (context.Clients.Any()) return;

                foreach (var client in clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }
        }
        public void SetUpScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var context = new ScopeConfigurationDbContext(
                options.ConnectionString, options.Schema))
            {
                if (context.Scopes.Any()) return;

                foreach (var scope in scopes)
                {
                    context.Scopes.Add(scope.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}
