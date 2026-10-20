using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProjetoFixacao
{
    public class RabbitMQExchange : IExchange
    {
        private readonly IBus _bus;

        public RabbitMQExchange(IBus bus)
        {

            _bus = bus;
        }
        public async Task PublicarAsync<T>(string routingkey, T mensagem) where T : class 
        {
            await _bus.Publish(mensagem);
        }
    }
}
