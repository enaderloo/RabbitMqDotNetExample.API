using RabbitMqDotNetProducer.API.Data;
using Xunit;

public class ProductTests
{
    public ProductTests()
    {
        
    }

    [Fact]
    public void AddProduct_ShouldAddProductToDatabase()
    {
        // Arrange
        var factory = new InMemoryDbContextFactory();
        using var context = factory.Create();

        var product = new Order { ProductName = "Test Product", Price = 9.99m };

        // Act
        context.Orders.Add(product);
        context.SaveChanges();

        // Assert
        Assert.Equal(1, context.Orders.Count());
        Assert.Equal("Test Product", context.Orders.First().ProductName);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var factory = new InMemoryDbContextFactory();
        using var context = factory.Create();

        context.Orders.AddRange(
            new Order { ProductName = "Product 1", Price = 1000m },
            new Order { ProductName = "Product 2", Price = 20000m }
        );
        context.SaveChanges();

        // Act
        var orders = context.Orders.ToList();

        // Assert
        Assert.Equal(2, orders.Count);
        Assert.Contains(orders, p => p.ProductName == "Product 1");
        Assert.Contains(orders, p => p.ProductName == "Product 2");
    }
}
