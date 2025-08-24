using ChatAnalysis.Application.Interfaces;
using ChatAnalysis.Application.Services;
using ChatAnalysis.Infrastructure.Integration;
using ChatAnlysis.Infrastructure.Integration;
using ChatAnlysis.Infrastructure.Interface;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Servi�os de infraestrutura
        services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
        services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

        // Servi�o de an�lise de sentimento
        services.AddSingleton<ISentimentAnalysisService, SentimentAnalysisService>();

        // Worker
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
