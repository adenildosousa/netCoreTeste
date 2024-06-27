using Autofac;
using WAGym.Domain.Manager;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.DependencyModule
{
    public class DomainDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionManager>().As<ISessionManager>();
            builder.RegisterType<TokenManager>().As<ITokenManager>();
            builder.RegisterType<LoginManager>().As<ILoginManager>();
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<ProfileUserManager>().As<IProfileUserManager>();
        }
    }
}
