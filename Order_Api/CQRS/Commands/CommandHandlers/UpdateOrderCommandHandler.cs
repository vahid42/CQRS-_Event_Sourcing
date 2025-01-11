

using Ardalis.GuardClauses;
using Order_Api.Events;
using Order_Api.Repository;

namespace Order_Api.CQRS.Commands.CommandHandlers
{
    public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
    {
        //private readonly IWriteOrderRepository repository;
        private readonly IEventStoreRepository eventStoreRepository;


        public UpdateOrderCommandHandler(IEventStoreRepository eventStoreRepository)
        {
           // this.repository = repository;
            this.eventStoreRepository = eventStoreRepository;
        }
        public async Task HandleAsync(UpdateOrderCommand command)
        {
            Guard.Against.Null(command);

            //await repository.UpdateOrderAsync(command.Order);
            await eventStoreRepository.PublishAsync(new OrderCreatedEvent(command.Order));
        }
    }
}
