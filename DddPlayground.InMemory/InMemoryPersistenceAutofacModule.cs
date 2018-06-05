using Autofac;

namespace DddPlayground.Persistence.InMemory
{
    public class InMemoryPersistenceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAggregateRepository>()
                .As<IUserAggregateRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
