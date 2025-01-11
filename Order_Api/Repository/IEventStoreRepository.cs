using Order_Api.Events;

namespace Order_Api.Repository
{
    public interface IEventStoreRepository
    {
        Task PublishAsync(IEvent message);
    }
}
