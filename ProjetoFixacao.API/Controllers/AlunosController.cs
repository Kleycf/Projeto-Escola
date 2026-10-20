using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFixacao.API.Data;
using ProjetoFixacao.API.DTOs;
using ProjetoFixacao.API.Models;

namespace ProjetoFixacao.API.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<IActionResult> ObterTodos()
        {
            var alunos = await _context.Alunos.Include(a => a.Notas).ToListAsync();

            var response = alunos.Select(a => new AlunoResponseDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Idade = DateTime.Now.Year - a.DataNascimento.Year,
                Notas = a.Notas.Select(n => n.Nota).ToList()

            });

            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> Cadastrar([FromBody] CadastrarAlunoDto dto)
        {
            var aluno = new AlunoModel
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterTodos), new { id = aluno.Id }, aluno);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> ObterPorId(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Notas)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(aluno == null) 
            {
                return NotFound();
            }

            var response = new AlunoResponseDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Idade = DateTime.Now.Year - aluno.DataNascimento.Year,
                Notas = aluno.Notas.Select(n => n.Nota).ToList()
            };

            return Ok(response);
        }

        [HttpPost("{id}/avaliar")]
        public async Task<IActionResult> AvaliarPorId(int id,[FromBody] AvaliarAlunoDto dto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if(aluno == null)
            {
                return NotFound();
            }
            if(dto.Nota<0 || dto.Nota>10)
            {
                return BadRequest("Nota deve ser entre 0 e 10.");
            }
            var nota = new NotaModel
            {
                AlunoId = id,
                Nota = dto.Nota
            };

            _context.Notas.Add(nota);

            await _context.SaveChangesAsync();
            return Ok("Aluno avaliado com sucesso!");
        }
    }
}  