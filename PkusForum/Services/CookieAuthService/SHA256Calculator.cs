using System.Security.Cryptography;
using System.Text;

namespace PkusForum.Services.CookieAuthService
{
    public static class Sha256Calculator
    {
        public static string GetSha256Value(string plaintext)
        {
            SHA256 sha256Calculator = SHA256.Create();
            byte[] hash = sha256Calculator.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
