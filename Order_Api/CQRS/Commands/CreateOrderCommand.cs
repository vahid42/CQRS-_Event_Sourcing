using Order_Api.Entities;

namespace Order_Api.CQRS.Commands
{
    public class CreateOrderCommand: ICommand
    {
        public Order Order { get; }

        public CreateOrderCommand(Order order)
        {
            Order = order;
        }
    }
}
