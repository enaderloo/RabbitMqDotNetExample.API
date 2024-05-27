using Microsoft.AspNetCore.Mvc;
using RabbitMqDotNetProducer.API.Data;
using RabbitMqDotNetProducer.API.Dto;
using RabbitMqDotNetProducer.API.RabbitMQ;


[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMessageProducer _messagePublisher;

    public OrdersController(IMessageProducer messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto, CancellationToken cancellationToken)
    {
        Order order = new()
        {
            ProductName = orderDto.ProductName,
            Price = orderDto.Price,
            Quantity = orderDto.Quantity
        };

        var factory = new InMemoryDbContextFactory();
        using var context = factory.Create();

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        _messagePublisher.SendMessage(order);

        return Ok(new { id = order.Id });
    }
}