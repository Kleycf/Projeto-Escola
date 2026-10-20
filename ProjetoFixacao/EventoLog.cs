using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoFixacao;

public class EventoLog
{
    public string Mensagem { get; init; }

    public string AlunoNome { get; init; }

    public EventoLog(string mensagem, string alunoNome)
    {
        Mensagem = mensagem;
        AlunoNome = alunoNome;
    }


}
