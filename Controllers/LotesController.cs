using CacauShowApi32427421.Data;
using CacauShowApi32427421.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CacauShowApi32427421.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> ListarLotes()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post(LoteProducao lote)
        {
           var produtoExiste = await _context.Produtos.AnyAsync(p => p.Id == lote.ProdutoId);
            if (!produtoExiste)
            {
                return BadRequest ($"Produto com ID {lote.ProdutoId} não encontrado.");
            }

            if (lote.Datafabricacao > DateTime.UtcNow.ToLocalTime())
            {
                return BadRequest ($"Data de fabricação {lote.Datafabricacao} não pode ser maior que a data atual.");
            }
            
            _context.Lotes.Add(lote);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Post), new { id = lote.Id }, lote);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> AtualizarStatus(int id, [FromBody] string novoStatus)
        {
            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                return NotFound($"Lote com ID {id} não encontrado.");
            }

            if (string.Equals(lote.Status, "Descartado", StringComparison.OrdinalIgnoreCase) &&
                (string.Equals(novoStatus, "Qualidade Aprovada", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(novoStatus, "Distribuído", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Regra de negócio: um lote descartado não pode ser alterado para Qualidade Aprovada ou Distribuído.");
            }

            lote.Status = novoStatus;
            await _context.SaveChangesAsync();
            return Ok(lote);
        }

    }
}