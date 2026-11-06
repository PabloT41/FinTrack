
using FinTrack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)

        {
            
        }

        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movimiento>()
            .Property(m => m.Monto)
            .HasPrecision(18, 2);
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
