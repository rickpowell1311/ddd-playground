using System.Threading.Tasks;

namespace DddPlayground.Domain.Infrastructure.EventSourcing
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEvent @event);
    }
}
