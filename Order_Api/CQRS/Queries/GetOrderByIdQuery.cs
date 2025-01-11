using Ardalis.GuardClauses;
using Order_Api.Entities;
using Order_Api.Repository;

namespace Order_Api.CQRS.Queries
{
    public class GetOrderByIdQuery : IQuery
    {
        public Guid orderId { get; }
        public GetOrderByIdQuery(Guid orderId)
        {
            this.orderId = orderId;
        }
        public class OrderbyIdQuery : IQueryHandler<GetOrderByIdQuery, Order>
        {
            private readonly IReadOrderRepository readOrderRepository;

            public OrderbyIdQuery(IReadOrderRepository readOrderRepository)
            {
                this.readOrderRepository = readOrderRepository;
            }
            public async Task<Order?> HandleAsync(GetOrderByIdQuery query)
            {
                Guard.Against.Default(query?.orderId);
                return await readOrderRepository.GetOrderByIdAsync(query.orderId);
            }
        }
    }
}
