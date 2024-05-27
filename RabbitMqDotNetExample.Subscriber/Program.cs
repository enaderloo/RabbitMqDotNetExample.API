// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "orders",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" Received {message}");
};

channel.BasicConsume(queue: "orders",
                             autoAck: true,
                             consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();