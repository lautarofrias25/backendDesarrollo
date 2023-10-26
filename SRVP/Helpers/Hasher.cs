using System.Security.Cryptography;
using System.Text;

namespace SRVP.Helpers;

public class Hasher
{
    public byte[] GenerateHash(string toHash)
    {
        SHA256 sha256hash = SHA256.Create();
        byte[] hash = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(toHash));
        return hash;
    }
}