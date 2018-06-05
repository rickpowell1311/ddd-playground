using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DddPlayground.Domain.Infrastructure.MediatR.EventSourcing
{
    public interface INotification<TEvent> : INotification
        where TEvent : IEvent
    {
        TEvent Event { get; set; }
    }
}
