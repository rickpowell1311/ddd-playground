using Autofac;
using DddPlayground.Domain.Infrastructure.EventSourcing;

namespace DddPlayground.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<EventSourcingAutofacModule>();
        }
    }
}
