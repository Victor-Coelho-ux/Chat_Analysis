using ChatAnalysis.Application.Interfaces;
using ChatAnalysis.Domain.DTO;
using ChatAnlysis.Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ChatAnalysis.Infrastructure.Integration
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly ISentimentAnalysisService _sentimentService;
        private IModel _channel; 

        public RabbitMQConsumer(IRabbitMQConnection connection, ILogger<RabbitMQConsumer> logger, ISentimentAnalysisService sentimentService)
        {
            _connection = connection;
            _logger = logger;
            _sentimentService = sentimentService;
        }

        public void StartConsuming(string queueName)
        {
            _channel = _connection.CreateChannel(); 

            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var dto = JsonSerializer.Deserialize<MessageDto>(message);

                    _sentimentService.Analyze(dto.Message, dto.ProductId);

                    _channel.BasicAck(ea.DeliveryTag, multiple: false); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[Consumer] Erro ao processar mensagem.");
                    _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            _channel.BasicConsume(queue: queueName,
                                  autoAck: false, 
                                  consumer: consumer);

            _logger.LogInformation($"[Consumer] Iniciado. Aguardando mensagens da fila '{queueName}'.");
        }
    }
}
