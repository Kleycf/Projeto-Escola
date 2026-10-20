using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFixacao
{
   
       public interface IExchange
    {
        Task PublicarAsync<T>(string routingKey, T Mensagem) where T : class;
    }
}

