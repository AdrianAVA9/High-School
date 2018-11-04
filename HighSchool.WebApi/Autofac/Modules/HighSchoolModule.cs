using Autofac;
using HighSchool.Data.Repositories;
using Management;
using System.Configuration;

namespace HighSchool.WebApi.Autofac.Modules
{
    public class HighSchoolModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new UnitOfWork(
                ConfigurationManager.ConnectionStrings["HighSchool"].ConnectionString))
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterType<UserManagement>().AsImplementedInterfaces().AsSelf();
            builder.RegisterType<ProfileManagement>().AsImplementedInterfaces().AsSelf();

            base.Load(builder);
        }
    }
}