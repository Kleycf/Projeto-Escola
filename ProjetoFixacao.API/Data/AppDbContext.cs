using Microsoft.EntityFrameworkCore;
using ProjetoFixacao.API.Models;

namespace ProjetoFixacao.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<AlunoModel> Alunos { get; set; } = null!;

        public DbSet<NotaModel> Notas { get; set; } = null!;

        public DbSet<LogModel> Logs { get; set; } = null!;
    }
}
