using System.Threading.Tasks;

namespace DddPlayground.Domain.Infrastructure.EventSourcing
{
    public class EventDispatcher : IEventDispatcher
    {
        public async Task Dispatch(IEvent @event)
        {
            // TODO:
            await Task.Yield();
        }
    }
}
