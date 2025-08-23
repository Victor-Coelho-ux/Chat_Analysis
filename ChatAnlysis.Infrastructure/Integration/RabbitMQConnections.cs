using ChatAnlysis.Infrastructure.Interface;
using RabbitMQ.Client;
using System;

namespace ChatAnlysis.Infrastructure.Integration
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(string hostName = "localhost", int port = 5672, string userName = "guest", string password = "guest")
        {
            var factory = new ConnectionFactory
            {
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password,
                AutomaticRecoveryEnabled = true
            };

            _connection = factory.CreateConnection();
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateChannel()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Não há conexão com RabbitMQ.");

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _connection?.Close();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}
