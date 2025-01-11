using Ardalis.GuardClauses;
using Order_Api.Repository;

namespace Order_Api.Events.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IWriteOrderRepository _repository;
        private readonly IReadOrderRepository _readOrdersRepository;

        public OrderCreatedEventHandler(IWriteOrderRepository repository, IReadOrderRepository readOrdersRepository)
        {
            _repository = repository;
            _readOrdersRepository = readOrdersRepository;
        }
        public async Task HandleAsync(OrderCreatedEvent @event)
        {
            Guard.Against.Null(@event);

            var order = await _readOrdersRepository.GetOrderByIdAsync(@event.Order.Id);
            if (order is not { })
            {
                await _repository.CreateOrderAsync(@event.Order);
            }
            else
            {
                await _repository.UpdateOrderAsync(@event.Order);
            }
        }
    }
}
