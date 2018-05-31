using DddPlayground.Infrastructure.Persistance;
using System.Threading.Tasks;

namespace DddPlayground.Persistence.InMemory
{
    public class InMemoryPersistenceScope : IPeristenceScope
    {
        public async Task BeginTransaction()
        {
            await Task.Yield();
        }

        public async Task CommitTransaction()
        {
            await Task.Yield();
        }
    }
}
