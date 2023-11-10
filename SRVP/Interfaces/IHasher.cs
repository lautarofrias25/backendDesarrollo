namespace SRVP.Interfaces
{
    public interface IHasher
    {
        string generateHash(byte[] toHash);
        string generateAccessCode();
        byte[] generateSalt();
    }
}
