using DddPlayground.Domain;
using DddPlayground.Infrastructure.EventSourcing;
using DddPlayground.Persistence;
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
            private readonly IUserAggregateRepository userRepository;

            public Handler(IEventDispatcher eventDispatcher, IUserAggregateRepository userRepository)
            {
                this.eventDispatcher = eventDispatcher;
                this.userRepository = userRepository;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var user = new User.Aggregate("Rick Powell");

                user = await this.userRepository.Insert(user);

                var fetched = await this.userRepository.Fetch(user.State.Id);
                fetched.ChangeName("Richard Powell", eventDispatcher);

                await this.userRepository.Update(fetched);
                await this.userRepository.Delete(fetched);
            }
        }
    }
}
