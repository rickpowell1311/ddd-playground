using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DddPlayground.Domain.Infrastructure.MediatR.EventSourcing
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, IEnumerable<Type>> eventsAndNotifications;
        private readonly IMediator mediator;

        public EventDispatcher(IMediator mediator, Dictionary<Type, IEnumerable<Type>> eventsAndNotifications)
        {
            this.mediator = mediator;
            this.eventsAndNotifications = eventsAndNotifications;
        }

        public async Task Dispatch(IEvent @event)
        {
            if (eventsAndNotifications.ContainsKey(@event.GetType()))
            {
                var eventNotifications = eventsAndNotifications[@event.GetType()];

                foreach (var eventNotification in eventNotifications)
                {
                    var notification = Activator.CreateInstance(eventNotification);

                    var properties = TypeDescriptor.GetProperties(notification);
                    foreach (PropertyDescriptor property in properties)
                    {
                        if (property.Name == "Event")
                        {
                            property.SetValue(notification, @event);
                        }
                    }

                    await this.mediator.Publish((INotification)notification);
                }
            }
        }
    }
}
