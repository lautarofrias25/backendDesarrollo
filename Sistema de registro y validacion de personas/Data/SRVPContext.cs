using Microsoft.EntityFrameworkCore;
using SRVP.Data.DTOs;

namespace SRVP.Data
{
    public class SRVPContext : DbContext
    {
        public SRVPContext(DbContextOptions<SRVPContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityAlwaysColumns();
        }
        public DbSet<Persona> Personas { get ; set; }
        public DbSet<SistemaExterno> SistemasExternos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
    }
}
