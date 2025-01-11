using Order_Api.Entities;

namespace Order_Api.Repository
{
    public interface IWriteOrderRepository
    {
        Task<Order> CreateOrderAsync(Order entity);
        Task<Order> UpdateOrderAsync(Order entity);
        Task<bool> DeleteOrderByIdAsync(Order entity);

    }
}
