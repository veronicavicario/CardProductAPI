namespace CardProductAPI.RabbitMQ.Producer;

public interface IMessageProducer
{
    void SendMessage<T> (T message, string queue);
}