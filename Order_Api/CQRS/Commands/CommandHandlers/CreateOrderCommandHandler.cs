
using Ardalis.GuardClauses;
using Order_Api.Repository;
using Order_Api.Repository.Implementation;
using static Order_Api.Events.OrderCreatedEvent;

namespace Order_Api.CQRS.Commands.CommandHandlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IEventStoreRepository eventStoreRepository;

        //private readonly IWriteOrderRepository repository;

        public CreateOrderCommandHandler(IEventStoreRepository eventStoreRepository)
        {
            this.eventStoreRepository = eventStoreRepository;
         }
        public async Task HandleAsync(CreateOrderCommand command)
        {
            Guard.Against.Null(command);

            //await repository.CreateOrderAsync(command.Order);
            await eventStoreRepository.PublishAsync(new OrderUpdatedEvent(command.Order));
        }
    }
    
}
