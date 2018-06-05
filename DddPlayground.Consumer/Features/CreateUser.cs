using DddPlayground.Domain;
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

        public class Handler : AsyncRequestHandler<Request>
        {
            private readonly IUserAggregateRepository userRepository;

            public Handler(IUserAggregateRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            protected async override Task Handle(Request request, CancellationToken cancellationToken)
            {
                var user = new User.Aggregate("Bob Smith");

                user = await this.userRepository.Insert(user);

                var fetched = await this.userRepository.Fetch(user.State.Id);
                fetched.ChangeName("Fred Bloggs");

                await this.userRepository.Update(fetched);
                await this.userRepository.Delete(fetched);
            }
        }
    }
}
