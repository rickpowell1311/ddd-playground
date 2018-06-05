using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DddPlayground.Domain;
using DddPlayground.Infrastrcuture;
using NPoco;

namespace DddPlayground.Persistence
{
    public class UserAggregateRepository : IUserAggregateRepository
    {
        private readonly IDatabase database;

        public UserAggregateRepository(IDatabase database)
        {
            this.database = database;
        }

        public async Task<User.Aggregate> Fetch(long id)
        {
            var dto = await database.SingleByIdAsync<Dtos.User>(id);
            return new User.Aggregate(dto.Map());
        }

        public async Task<User.Aggregate> Insert(User.Aggregate aggregate)
        {
            var dto = new Dtos.User();
            dto.Populate(aggregate.State);
            dto.Id = (long)await database.InsertAsync(dto);

            var state = dto.Map();
            state.HasNewIdentity = true;

            return new User.Aggregate(state);
        }

        public async Task Delete(User.Aggregate aggregate)
        {
            var dto = new Dtos.User();
            dto.Populate(aggregate.State);

            await database.DeleteAsync(dto);
        }

        public async Task Update(User.Aggregate aggregate)
        {
            var dto = new Dtos.User();
            dto.Populate(aggregate.State);

            await database.UpdateAsync(dto);
        }

        public static class Dtos
        {
            public class User : IPopulatableFrom<Domain.User.UserState>, IMappableTo<Domain.User.UserState>
            {
                public long Id { get; set; }

                public string Name { get; set; }

                public Domain.User.UserState Map()
                {
                    return new Domain.User.UserState
                    {
                        Id = Id,
                        Name = Name
                    };
                }

                public void Populate(Domain.User.UserState source)
                {
                    Id = source.Id;
                    Name = source.Name;
                }
            }
        }
    }
}
