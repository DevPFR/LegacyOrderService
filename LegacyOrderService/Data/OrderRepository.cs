using LegacyOrderService.Models;
using Microsoft.EntityFrameworkCore;

namespace LegacyOrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task SeedBadDataAsync()
        {
            var order = new Order
            {
                CustomerName = "John",
                ProductName = "Widget",
                Quantity = 9999,
                Price = 9.99
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}