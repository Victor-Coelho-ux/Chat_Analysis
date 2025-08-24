namespace ChatAnalysis.Infrastructure.Integration
{
    public interface IRabbitMQConsumer
    {
        void StartConsuming(string queueName);
    }
}
