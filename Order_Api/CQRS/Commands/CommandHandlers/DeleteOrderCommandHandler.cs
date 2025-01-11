
using Ardalis.GuardClauses;
using Order_Api.Events;
using Order_Api.Repository;

namespace Order_Api.CQRS.Commands.CommandHandlers
{
    public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
    {
        //private readonly IWriteOrderRepository repository;
        private readonly IEventStoreRepository eventStoreRepository;



        public DeleteOrderCommandHandler(IEventStoreRepository eventStoreRepository)
        {
            this.eventStoreRepository = eventStoreRepository;
            // this.repository = repository;
        }
        public async Task HandleAsync(DeleteOrderCommand command)
        {
            Guard.Against.Null(command);

            // await repository.DeleteOrderByIdAsync(command.Order);
            await eventStoreRepository.PublishAsync(new OrderCreatedEvent(command.Order));
        }
    }
}
