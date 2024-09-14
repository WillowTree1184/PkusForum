using System.Diagnostics.CodeAnalysis;

namespace PkusForum.Services.CookieAuthService
{
    public static class TokenDictionaryService
    {
        static Dictionary<string, string> tokenDictionary = new Dictionary<string, string>();

        public static string Regist(string token)
        {
            string tokenId = Sha256Calculator.GetSha256Value($"{DateTime.Now.Ticks}{Guid.NewGuid()}TokenId{Random.Shared.Next()}{Sha256Calculator.GetSha256Value($"{Guid.NewGuid()}{token}{DateTime.Now}{Random.Shared.Next()}")}{Random.Shared.Next()}{Random.Shared.Next()}");   //Use complex and unique strings to generate tokenId
            tokenDictionary.Add(tokenId, token);
            return tokenId;
        }

        public static bool TryPopToken(string tokenId, [MaybeNullWhen(false)] out string token) => tokenDictionary.TryGetValue(tokenId, out token);
    }
}
