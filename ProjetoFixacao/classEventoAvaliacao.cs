using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoFixacao;

public class EventoAvaliacao
{
    public string Nome { get; init; }

    public int Nota { get; init; }
    public DateTime Data { get;}

    public EventoAvaliacao(string nome, int nota)
    {
        Nome = nome;
        Nota = nota;
        Data = DateTime.Now;
    }   


}
