using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ChatAnalysis.Infrastructure.Integration;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitMQConsumer _consumer;

    public Worker(ILogger<Worker> logger, IRabbitMQConsumer consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker iniciado. Aguardando mensagens...");

        _consumer.StartConsuming("message_analysis");

        return Task.CompletedTask;
    }
}
