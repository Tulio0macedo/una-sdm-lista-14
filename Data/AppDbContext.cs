using Microsoft.EntityFrameworkCore;
using CacauShowApi32427421.Models;

namespace CacauShowApi32427421.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Franquia> Franquias => Set<Franquia>();
    public DbSet<LoteProducao> Lotes => Set<LoteProducao>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Produto> Produtos => Set<Produto>();
}