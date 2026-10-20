namespace ProjetoFixacao.API.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        public string Mensagem { get; set; } = string.Empty;

        public DateTime DataHora { get; set; }

        public string AlunoNome { get; set; } = string.Empty;
    }
}
