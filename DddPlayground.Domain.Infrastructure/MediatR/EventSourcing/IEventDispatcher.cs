using System.Threading.Tasks;

namespace DddPlayground.Domain.Infrastructure.MediatR.EventSourcing
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEvent @event);
    }
}
