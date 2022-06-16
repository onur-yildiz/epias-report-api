namespace SP.Utils.Cryptography
{
    public interface ICryptographyUtils
    {
        (string, byte[]) Encrypt(string password, byte[]? salt = null);
        bool IsPasswordCorrect(string password, string hashedPassword, byte[] salt);
    }
}