using DddPlayground.Infrastructure.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DddPlayground.Infrastrcuture.EventSourcing
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
