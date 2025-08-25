using ChatAnalysis.Application.Interfaces;
using ChatAnalysis.Application.Services;
using ChatAnalysis.Infrastructure.Integration;
using ChatAnlysis.Infrastructure.Integration;
using ChatAnlysis.Infrastructure.Interface;
using ChatAnlysis.Infrastructure.Repositories; 

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Serviços de infraestrutura
        services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
        services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

        // Repositório de ProductAnalysis (injeção da connection string)
        string connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddSingleton<IProductAnalysisRepository>(sp => new ProductAnalysisRepository(connectionString));

        // Serviço de análise de sentimento
        services.AddSingleton<ISentimentAnalysisService, SentimentAnalysisService>();

        // Worker
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
