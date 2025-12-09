using Microsoft.EntityFrameworkCore;
using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}