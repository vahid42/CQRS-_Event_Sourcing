using Ardalis.GuardClauses;
using Order_Api.Repository;
using static Order_Api.Events.OrderCreatedEvent;

namespace Order_Api.Events.EventHandlers
{
    public class OrderUpdatedEventHandler : IEventHandler<OrderUpdatedEvent>
    {
        private readonly IWriteOrderRepository _repository;

        public OrderUpdatedEventHandler(IWriteOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(OrderUpdatedEvent @event)
        {
            Guard.Against.Null(@event);

            await _repository.UpdateOrderAsync(@event.Order);
        }
    }

}
