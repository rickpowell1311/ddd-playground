using System;
using System.Collections.Generic;
using System.Text;

namespace DddPlayground.Domain.Infrastructure.MediatR.EventSourcing
{
    public static class EventRaiser
    {
        public static event EventHandler<IEvent> EventRaised;

        public static void RaiseEvent(object sender, IEvent @event)
        {
            EventRaised?.Invoke(sender, @event);
        }
    }
}
