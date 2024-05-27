using Microsoft.EntityFrameworkCore;

namespace RabbitMqDotNetProducer.API.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
        {
        }
    }
}
