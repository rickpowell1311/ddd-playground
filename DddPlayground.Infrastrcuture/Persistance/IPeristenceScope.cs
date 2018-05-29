using System.Threading.Tasks;

namespace DddPlayground.Infrastructure.Persistance
{
    public interface IPeristenceScope
    {
        Task BeginTransaction();

        Task CommitTransaction();
    }
}
