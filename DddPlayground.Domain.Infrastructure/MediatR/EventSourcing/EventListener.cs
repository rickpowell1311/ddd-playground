using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DddPlayground.Domain.Infrastructure.MediatR.EventSourcing
{
    public class EventListener
    {
        private readonly IEventDispatcher eventDispatcher;

        public EventListener(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void HandleRaisedEvent(object sender, IEvent @event)
        {
            this.eventDispatcher.Dispatch(@event);
        }
    }
}
