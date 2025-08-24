using ChatAnalysis.Domain.DTO;
using ChatAnalysis.Domain.Interface;
using ChatAnalysis.Domain.Serialization;
using ChatAnlysis.Infrastructure.Interface;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatAnlysis.Application.Service
{
    public class QueueMessageService : IQueueMessagesService
    {
        private readonly IRabbitMQProducer _producer;
        private readonly string _queueName;

        public QueueMessageService(IRabbitMQProducer producer, string queueName = "message_analysis")
        {
            _producer = producer ?? throw new System.ArgumentNullException(nameof(producer));
            _queueName = queueName;
        }

        public Task PublishMessageForAnalysisAsync(MessageDto message)
        {
            if (message == null) throw new System.ArgumentNullException(nameof(message));

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            _producer.Publish(message, _queueName);

            return Task.CompletedTask;
        }
    }
}
