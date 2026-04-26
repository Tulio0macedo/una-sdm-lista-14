using CacauShowApi32427421.Data;
using CacauShowApi32427421.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApi32427421.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FranquiasController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> ListarFranquias()
        {
            var franquias = await _context.Franquias.ToListAsync();
            return Ok(franquias);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Franquia franquia)
        {
            _context.Franquias.Add(franquia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { id = franquia.Id }, franquia);
        }
    }
}
