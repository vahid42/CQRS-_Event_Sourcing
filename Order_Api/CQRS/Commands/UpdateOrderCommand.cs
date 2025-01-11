using Ardalis.GuardClauses;
using Order_Api.CQRS.Commands.CommandHandlers;
using Order_Api.Entities;
using Order_Api.Repository;

namespace Order_Api.CQRS.Commands
{
    public class UpdateOrderCommand : ICommand
    {
        public Order Order { get; }

        public UpdateOrderCommand(Order order)
        {
            Order = order;
        }
    }
}
