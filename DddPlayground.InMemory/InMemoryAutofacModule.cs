using Autofac;

namespace DddPlayground.InMemory
{
    public class InMemoryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryPersistenceScope>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserAggregateRepository>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
