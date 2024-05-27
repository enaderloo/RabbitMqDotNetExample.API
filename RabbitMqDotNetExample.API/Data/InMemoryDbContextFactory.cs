namespace RabbitMqDotNetProducer.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class InMemoryDbContextFactory
    {
        public OrderDbContext Create()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var context = new OrderDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }

}
