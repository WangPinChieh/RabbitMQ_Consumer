using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() {HostName = "localhost"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("hello", false, false, false, null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (modal, ea) =>
{
    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
    Console.WriteLine($"Message Received: {message}");
};
channel.BasicConsume("hello", true, consumer);


Console.ReadLine();