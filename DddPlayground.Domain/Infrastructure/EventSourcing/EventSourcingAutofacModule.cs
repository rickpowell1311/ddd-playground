using Autofac;

namespace DddPlayground.Domain.Infrastructure.EventSourcing
{
    public class EventSourcingAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IEventDispatcher>().As<IEventDispatcher>().SingleInstance();
        }
    }
}
