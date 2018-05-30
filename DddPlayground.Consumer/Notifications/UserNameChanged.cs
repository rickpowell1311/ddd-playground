using DddPlayground.Domain;
using DddPlayground.Infrastrcuture.EventSourcing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DddPlayground.Consumer.Notifications
{
    public static class UserNameChanged
    {
        public class Notification : INotification<User.NameChanged>
        {
            public User.NameChanged Event { get; set; }
        }

        public class NotificationHandler : INotificationHandler<Notification>
        {
            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                await Task.Yield();

                return;
            }
        }
    }
}
