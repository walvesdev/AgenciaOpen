using AgenciaOpen.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgenciaOpen.Database
{
    public class AgenciaOpenContext : DbContext
    {
        public AgenciaOpenContext(DbContextOptions<AgenciaOpenContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemsPedido { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().HasKey(m => m.Id);
            builder.Entity<Pedido>().HasKey(m => m.Id);
            builder.Entity<ItemPedido>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
