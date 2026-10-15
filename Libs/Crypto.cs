using System.Security.Cryptography;
using System.Text;

namespace Libs;

public static class Crypto
{
    public static string Hash(string text)
    {
        byte[] sourceBytes = Encoding.UTF8.GetBytes(text);
        byte[] hashBytes = SHA256.HashData(sourceBytes);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}