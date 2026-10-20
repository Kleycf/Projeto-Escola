namespace ProjetoFixacao.API.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public DateOnly DataNascimento { get; set; }


        public List<NotaModel> Notas { get; set; } = new();


    }
}
