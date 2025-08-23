using ChatAnlysis.Infrastructure.Interface;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using ChatAnalysis.Domain.Serialization;

namespace ChatAnlysis.Infrastructure.Integration
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;

        public RabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Publish<T>(T message, string queueName)
        {
            if (!_connection.IsConnected)
                throw new InvalidOperationException("Não há conexão com RabbitMQ.");

            using IModel channel = _connection.CreateChannel();

            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var json = JsonSerializer.Serialize(message, AppJsonSerializerContext.Default.GetTypeInfo(message.GetType()));
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body
            );
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
