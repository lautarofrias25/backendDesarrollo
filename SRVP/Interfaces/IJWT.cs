using SRVP.Data.Models;

namespace SRVP.Interfaces
{
    public interface IJWT
    {
        string GenerateToken(Persona user);
    }
}
