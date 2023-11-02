using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace SRVP.Helpers;

public class Hasher
{
    private readonly string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public string generateHash(byte[] toHash)
    {
        SHA256 sha256 = SHA256.Create();
        var hash = Convert.ToBase64String(sha256.ComputeHash(toHash));
        return hash;
    }
    public string generateAccessCode()
    { 
        StringBuilder stringBuilder = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < 40; i++)
        {
            int index = random.Next(caracteresPermitidos.Length);
            stringBuilder.Append(caracteresPermitidos[index]);
        }
        return stringBuilder.ToString();
    }
    public byte[] generateSalt() 
    {
        // Genera una salt de 16 bytes (128 bits)
        byte[] salt = new byte[16];

        // Utilizo RandomNumberGenerator para generar números aleatorios seguros

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Convierte la salt en una cadena Base64 para almacenarla en la base de datos
        return (salt);
    }
}