using System.Text.Json;

namespace UWRESTProject
{
    public static class TokenHelper
    {
        public static string GetToken(string email, string password)
        {
            // create the token
            var token = new Token
            {
                UserEmail = email,
                Expires = DateTime.UtcNow.AddMinutes(10),
            };

            var jsonString = JsonSerializer.Serialize(token);

            var encryptedJsonString = Crypto.EncryptStringAES(jsonString);

            return encryptedJsonString;
        }

        
        public static Token DecodeToken(string token)
        {
            var decryptedJsonString = Crypto.DecryptStringAES(token);

            var tokenObject = JsonSerializer.Deserialize<Token>(decryptedJsonString);

            if (tokenObject.Expires < DateTime.UtcNow)
            {
                return null;
            }

            return tokenObject;
        }
    }
}
