using DddPlayground.Domain;
using DddPlayground.Infrastructure.Persistance;
using DddPlayground.InMemory;
using System.Threading.Tasks;

namespace DddPlayground.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IPeristenceScope persistenceScope = new InMemoryPersistenceScope();

            var user = new User.Aggregate("Rick Powell");

            await persistenceScope.BeginTransaction();

            var repository = new UserAggregateRepository();
            user = await repository.Insert(user);

            await persistenceScope.CommitTransaction();
        }
    }
}
