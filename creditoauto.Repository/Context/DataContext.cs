using creditoauto.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace creditoauto.Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ejecutivo>()
            .Ignore(e=>e.CodigoPatio);

            modelBuilder.Entity<Ejecutivo>()
            .HasOne(p => p.Patio)
            .WithMany(b => b.Ejecutivos);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Ejecutivo> Ejecutivos { get; set; }
        public DbSet<ClientePatio> ClientePatios { get; set; }
        public DbSet<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
