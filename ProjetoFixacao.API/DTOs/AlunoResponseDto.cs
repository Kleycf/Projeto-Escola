namespace ProjetoFixacao.API.DTOs
{
    public class AlunoResponseDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public int Idade { get; set; }

        public List<int> Notas { get; set; } = new();
    }
}
