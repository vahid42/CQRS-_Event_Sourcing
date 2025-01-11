
using Order_Api.Entities;
using Order_Api.Repository;

namespace Order_Api.CQRS.Queries
{
    public class GetOrderQuery : IQuery
    {
        public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, IEnumerable<Order>>
        {
            private readonly IReadOrderRepository readOrderRepository;

            public GetOrderQueryHandler(IReadOrderRepository readOrderRepository)
            {
                this.readOrderRepository = readOrderRepository;
            }
            public async Task<IEnumerable<Order>> HandleAsync(GetOrderQuery query)
            {
               return await readOrderRepository.GetOrdersAsync();
            }
        }
    }
}
