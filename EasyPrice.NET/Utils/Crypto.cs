using System.Security.Cryptography;
using System;
using System.Text;

namespace EasyPrice.NET.Utils;

public static class Crypto
{
    public static string Hash(string text)
    {
        byte[] sourceBytes = Encoding.UTF8.GetBytes(text);
        byte[] hashBytes = SHA256.HashData(sourceBytes);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}