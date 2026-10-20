namespace ProjetoFixacao.API.Models
{
    public class NotaModel
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }

        public int Nota { get; set; }

        public AlunoModel Aluno { get; set; } = null!;
    }
}
