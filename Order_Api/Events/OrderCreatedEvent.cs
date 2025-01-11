using Order_Api.Entities;

namespace Order_Api.Events
{
    public partial class OrderCreatedEvent : IEvent
    {
        public Order Order { get; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}
