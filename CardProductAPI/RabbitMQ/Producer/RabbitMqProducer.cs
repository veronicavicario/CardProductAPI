using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace CardProductAPI.RabbitMQ.Producer;

public class RabbitMqProducer : IMessageProducer
{
    private readonly IModel _channel;
    public RabbitMqProducer(IModel channel)
    {
        _channel = channel;
    }
    public void SendMessage<T>(T message, string queue)
    {
        string json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: queue, body: body);
    }
}