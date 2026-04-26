using CacauShowApi32427421.Data;
using CacauShowApi32427421.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApi32427421.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pedido pedido)
        {
            var unidade = await _context.Franquias.FindAsync(pedido.UnidadeId);
            if (unidade == null)
            {
                return BadRequest($"Unidade com ID {pedido.UnidadeId} não encontrada.");
            }

            var produto = await _context.Produtos.FindAsync(pedido.ProdutoId);
            if (produto == null)
            {
                return BadRequest($"Produto com ID {pedido.ProdutoId} não encontrado.");
            }

            if (string.Equals(produto.Tipo, "Sazonal", StringComparison.OrdinalIgnoreCase))
            {
                pedido.ValorTotal += 15.00m;
                Console.WriteLine("Produto sazonal detectado: Adicionando embalagem de presente premium!");
            }

            var totalQuantidade = await _context.Pedidos
                .Where(p => p.UnidadeId == pedido.UnidadeId)
                .SumAsync(p => p.Quantidade);

            if (totalQuantidade + pedido.Quantidade > unidade.CapacidadeDeEstoque)
            {
                return BadRequest("Capacidade logística da loja excedida. Não é possível receber mais produtos.");
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { id = pedido.Id }, pedido);
        }
    }
}
