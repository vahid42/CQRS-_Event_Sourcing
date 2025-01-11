using Order_Api.Entities;

namespace Order_Api.CQRS.Commands
{
    public class DeleteOrderCommand : ICommand
    {
        public Order Order { get; }

        public DeleteOrderCommand(Order order)
        {
            Order = order;
        }
    }
}
