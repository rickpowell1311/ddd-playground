using DddPlayground.Domain;
using DddPlayground.Infrastructure.EventSourcing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DddPlayground.Consumer.Features
{
    public static class CreateUser
    {
        public class Request : IRequest
        {
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventDispatcher eventDispatcher;

            public Handler(IEventDispatcher eventDispatcher)
            {
                this.eventDispatcher = eventDispatcher;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var user = new User.Aggregate("Rick Powell");

                user.ChangeName("Richard Powell", this.eventDispatcher);
            }
        }
    }
}
