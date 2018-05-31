using Autofac;

namespace DddPlayground.Persistence.InMemory
{
    public class InMemoryPersistenceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryPersistenceScope>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserAggregateRepository>()
                .As<IUserAggregateRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
