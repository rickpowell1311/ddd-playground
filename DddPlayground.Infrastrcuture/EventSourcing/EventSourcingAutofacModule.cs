using Autofac;
using DddPlayground.Infrastrcuture.EventSourcing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DddPlayground.Infrastructure.EventSourcing
{
    public class EventSourcingAutofacModule : Module
    {
        private readonly System.Reflection.Assembly[] scannedAssemblies;

        public EventSourcingAutofacModule()
        {
            scannedAssemblies = new System.Reflection.Assembly[0];
        }

        public EventSourcingAutofacModule(params System.Reflection.Assembly[] scannedAssemblies)
        {
            this.scannedAssemblies = scannedAssemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var events = scannedAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEvent).IsAssignableFrom(t) && t.IsClass);

            var genericNotificationTypeDefinition = typeof(INotification<>);

            var notifications = scannedAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotification<>)))
                .Select(t => new
                {
                    Notification = t,
                    EventType = t.GetInterfaces().Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotification<>)).GetGenericArguments().Single()
                });

            var eventsAndNotifications = notifications
                .GroupBy(n => n.EventType)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Notification));

            var eventDispatcher = 

            builder.Register<EventDispatcher>(c => new EventDispatcher(c.Resolve<IMediator>(), eventsAndNotifications))
                .As<IEventDispatcher>()
                .SingleInstance();

            builder.Register(c =>
            {
                var eventListener = new EventListener(c.Resolve<IEventDispatcher>());
                EventRaiser.EventRaised += eventListener.HandleRaisedEvent;

                return eventListener;
            })
            .AsSelf()
            .SingleInstance()
            .AutoActivate();
        }
    }
}
