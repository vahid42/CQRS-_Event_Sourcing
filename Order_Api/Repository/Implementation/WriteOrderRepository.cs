using Order_Api.Data;
using Order_Api.Entities;

namespace Order_Api.Repository.Implementation
{
    public class WriteOrderRepository : IWriteOrderRepository
    {
        private readonly AppDbContext context;

        public WriteOrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Order> CreateOrderAsync(Order entity)
        {
            await context.Orders.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteOrderByIdAsync(Order entity)
        {
             context.Orders.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Order> UpdateOrderAsync(Order entity)
        {
            context.Orders.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
