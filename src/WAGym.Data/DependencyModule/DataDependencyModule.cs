using Autofac;
using WAGym.Data.Repository;
using WAGym.Data.Repository.Interface;

namespace WAGym.Data.DependencyModule
{
    public class DataDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<FunctionalityProfileRepository>().As<IFunctionalityProfileRepository>();
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
            builder.RegisterType<ProfileUserRepository>().As<IProfileUserRepository>();
        }
    }
}
