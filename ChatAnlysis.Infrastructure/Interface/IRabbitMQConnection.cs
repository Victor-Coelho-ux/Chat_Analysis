using RabbitMQ.Client;
using System;

namespace ChatAnlysis.Infrastructure.Interface
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }

        /// <summary>
        /// Cria e retorna um canal RabbitMQ (IModel) para publicar ou consumir mensagens.
        /// </summary>
        IModel CreateChannel();
    }
}
