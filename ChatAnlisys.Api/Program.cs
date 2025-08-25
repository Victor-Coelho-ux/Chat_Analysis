using ChatAnalysis.Domain.DTO;
using ChatAnalysis.Domain.Interface;
using ChatAnlysis.Application.Service;
using ChatAnlysis.Infrastructure.Integration;
using ChatAnlysis.Infrastructure.Interface;
using ChatAnlysis.Infrastructure.Repositories;
using System.Text.Json;
using ChatAnalysis.Domain.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;

        // Usa somente o Source Generator (sem reflexão)
        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    });

// Lê a connection string do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar repositório do MySQL via DI
builder.Services.AddScoped<IProductAnalysisRepository>(_ =>
    new ProductAnalysisRepository(connectionString));

// Registrar RabbitMQ Connection
builder.Services.AddSingleton<IRabbitMQConnection>(sp =>
{
    return new RabbitMQConnection(
        hostName: "localhost",
        port: 5672,
        userName: "guest",
        password: "guest"
    );
});

// Registrar RabbitMQ Producer
builder.Services.AddSingleton<IRabbitMQProducer>(sp =>
{
    var connection = sp.GetRequiredService<IRabbitMQConnection>();
    return new RabbitMQProducer(connection);
});

// Registrar QueueMessageService
builder.Services.AddScoped<IQueueMessagesService, QueueMessageService>();

var app = builder.Build();

// Mapear Controllers
app.MapControllers();

app.Run();
