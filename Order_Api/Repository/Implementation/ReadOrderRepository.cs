using Microsoft.EntityFrameworkCore;
using Order_Api.Data;
using Order_Api.Entities;

namespace Order_Api.Repository.Implementation
{
    public class ReadOrderRepository : IReadOrderRepository
    {
        private readonly AppDbContext context;

        public ReadOrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await context.Orders.Where(c=>c.Id==orderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await context.Orders.ToListAsync();
        }
    }
}
