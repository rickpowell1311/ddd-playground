using System.Threading.Tasks;

namespace DddPlayground.Infrastructure.EventSourcing
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEvent @event);
    }
}
