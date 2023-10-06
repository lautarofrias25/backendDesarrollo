using Microsoft.EntityFrameworkCore;

namespace Sistema_de_registro_y_validacion_de_personas.Data
{
    public class SRVPContext : DbContext
    {
        public SRVPContext(DbContextOptions<SRVPContext> options) : base(options) { }
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
