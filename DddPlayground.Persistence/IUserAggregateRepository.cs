using DddPlayground.Domain;
using System;
using System.Threading.Tasks;

namespace DddPlayground.Persistence
{
    public interface IUserAggregateRepository
    {
        Task<User.Aggregate> Fetch(long id);

        Task<User.Aggregate> Insert(User.Aggregate aggregate);

        Task Delete(User.Aggregate aggregate);

        Task Update(User.Aggregate aggregate);
    }
}
