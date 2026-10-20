using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFixacao.API.Data;

namespace ProjetoFixacao.API.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> ObterLogs()
        {
            var log = await _context.Logs.ToListAsync();
          
            return Ok(log);
        }
    }
}
