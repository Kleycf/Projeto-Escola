using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProjetoFixacao.API.Data;
using ProjetoFixacao.API.Models;

namespace ProjetoFixacao
{
    public class ConsumerLog:IConsumer<EventoLog>
    {
        private readonly AppDbContext _context;

        public ConsumerLog(AppDbContext context)
        {
            _context = context;
        }


        public async Task Consume(ConsumeContext<EventoLog> context)
        {
            var log = new LogModel
            {
                Mensagem = context.Message.Mensagem,
                AlunoNome = context.Message.AlunoNome,
                DataHora = DateTime.Now
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

        }
    }
}
