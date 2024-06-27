using Autofac;
using WAGym.Data.DependencyModule;
using WAGym.Domain.DependencyModule;

namespace WAGym.AccessControl.DependencyModule
{
    public class AccessControlDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.RegisterModule<DomainDependencyModule>();
            builder.RegisterModule<DataDependencyModule>();
        }
    }
}
