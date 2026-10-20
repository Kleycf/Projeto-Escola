using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace ProjetoFixacao
{
    public class classSubscribe : IConsumer<EventoAvaliacao>
    {

        private readonly IExchange _exchange;

        public  classSubscribe(IExchange exchange)
            {
                _exchange = exchange;
            }
        public async Task Consume(ConsumeContext<EventoAvaliacao> context)
        {
            var evento = context.Message;
            string mensagem = $"[Log] O aluno {evento.Nome} obteve nota {evento.Nota} em {evento.Data} !";
            await _exchange.PublicarAsync("log", new EventoLog(mensagem,evento.Nome));

        }
    }
}
