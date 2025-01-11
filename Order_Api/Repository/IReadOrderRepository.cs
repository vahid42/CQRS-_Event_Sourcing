using Order_Api.Entities;

namespace Order_Api.Repository
{
    public interface IReadOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid orderId);
    }
}
