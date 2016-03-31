using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace CarrinhodeCompras.Models
{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().Property(x => x.Descricao).IsRequired();

            var pedidoItem = modelBuilder.Entity<PedidoItem>();
            pedidoItem.Ignore(x => x.Selecionado);
            pedidoItem.HasOne(p => p.Pedido)
                      .WithMany(b => b.Itens)
                      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(b => b.Produtos)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}