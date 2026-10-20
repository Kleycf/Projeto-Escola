namespace ProjetoFixacao;

public class Aluno
{ 
    public string Nome { get; init; }

    public int Idade { get; }


    private List<int> notaAvaliacao;
    public Aluno(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
        notaAvaliacao = new List<int>();
    }

    public void AdicionarNota(int nota)
    {
       notaAvaliacao.Add(nota); 
    }

    public IReadOnlyList<int> ObterNotas()
    {
        return notaAvaliacao.AsReadOnly();
    }

    public double CalcularMedia()
    {
        if(notaAvaliacao.Count == 0)
        {
            return 0;
        }
        else
        {
            return notaAvaliacao.Average();
        }
    }
}