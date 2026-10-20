using MassTransit.Transports.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFixacao
{
   /* public class Exchange : IExchange
    {
        private readonly Dictionary<string, IMessageQueue> _dicFilas;

        public Exchange()
        {
            _dicFilas = new Dictionary<string, IMessageQueue>();
        }

        public async Task PublicarAsync<T>(string routingKey, T mensagem) where T : class
        {
            if (_dicFilas.TryGetValue(routingKey, out var fila))
            {
                var filaCast = (MessageQueue<T>)fila;
                await filaCast.Writer.WriteAsync(mensagem);
            }
            else
            {
                throw new Exception($"Não existe fila para a routing key: {routingKey}");
            } 
        }
    } */
}
