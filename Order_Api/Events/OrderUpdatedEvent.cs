using Order_Api.Entities;

namespace Order_Api.Events
{
    public partial class OrderCreatedEvent
    {
        public class OrderUpdatedEvent : IEvent
        {
            public Order Order { get; }

            public OrderUpdatedEvent(Order order)
            {
                Order = order;
            }
        }
    }
}
