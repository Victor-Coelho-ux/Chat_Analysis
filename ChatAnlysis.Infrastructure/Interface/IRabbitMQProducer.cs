using System;

namespace ChatAnlysis.Infrastructure.Interface
{
    public interface IRabbitMQProducer : IDisposable
    {
        /// <summary>
        /// 
        /// Publica uma mensagem na fila especificada.
        /// </summary>
        /// <typeparam name="T">Tipo da mensagem</typeparam>
        /// <param name="message">Mensagem a ser enviada</param>
        /// <param name="queueName">Nome da fila</param>
        void Publish<T>(T message, string queueName);
    }
}
