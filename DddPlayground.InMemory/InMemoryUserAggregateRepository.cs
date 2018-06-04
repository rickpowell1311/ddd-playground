using DddPlayground.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddPlayground.Persistence.InMemory
{
    public class UserAggregateRepository : IUserAggregateRepository
    {
        private Dictionary<long, User.UserState> _inMemoryStore;

        public UserAggregateRepository()
        {
            _inMemoryStore = new Dictionary<long, User.UserState>();
        }

        public async Task<User.Aggregate> Fetch(long id)
        {
            await Task.Yield();

            return new User.Aggregate(_inMemoryStore[id]);
        }

        public async Task<User.Aggregate> Insert(User.Aggregate aggregate)
        {
            await Task.Yield();

            _inMemoryStore[aggregate.State.Id] = aggregate.State;

            return new User.Aggregate(aggregate.State);
        }

        public async Task Delete(User.Aggregate aggregate)
        {
            if (_inMemoryStore.ContainsKey(aggregate.State.Id))
            {
                _inMemoryStore.Remove(aggregate.State.Id);
            }

            await Task.Yield();
        }

        public async Task Update(User.Aggregate aggregate)
        {
            _inMemoryStore[aggregate.State.Id] = aggregate.State;

            await Task.Yield();
        }
    }
}
