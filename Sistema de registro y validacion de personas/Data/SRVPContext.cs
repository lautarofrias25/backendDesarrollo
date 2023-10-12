using Microsoft.EntityFrameworkCore;
using Sistema_de_registro_y_validacion_de_personas.Data.Tables;

namespace Sistema_de_registro_y_validacion_de_personas.Data
{
    public class SRVPContext : DbContext
    {
        public SRVPContext(DbContextOptions<SRVPContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityAlwaysColumns();
        }
        public DbSet<PersonaTable> Personas { get ; set; }
        public DbSet<SistemaExternoTable> SistemasExternos { get; set; }
        public DbSet<UsuarioTable> Usuarios { get; set; }
        
    }
}
