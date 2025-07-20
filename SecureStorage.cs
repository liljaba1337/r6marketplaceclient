using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace r6marketplaceclient
{
    internal class SecureStorageFormat : IDisposable
    {
        public void Dispose()
        {
            Email = null;
            Password = null;
            Token = null;
        }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
    internal static class SecureStorage
    {
        internal static SecureStorageFormat? Decrypt()
        {
            if (!File.Exists("data/secret.dat")) return null;
            byte[] encryptedData = File.ReadAllBytes("data/secret.dat");
            string decryptedData = _Decrypt(encryptedData);
            SecureStorageFormat? secureStorageFormat = System.Text.Json.JsonSerializer.Deserialize<SecureStorageFormat>(decryptedData);
            return secureStorageFormat;
        }
        internal static void Encrypt(string email, string password, string token)
        {
            using SecureStorageFormat secureStorageFormat = new SecureStorageFormat
            {
                Email = email,
                Password = password,
                Token = token
            };
            string jsonData = System.Text.Json.JsonSerializer.Serialize(secureStorageFormat);
            byte[] encryptedData = _Encrypt(jsonData);
            Directory.CreateDirectory("data");
            File.WriteAllBytes("data/secret.dat", encryptedData);
        }
        internal static void Encrypt(string field, string data)
        {
            using SecureStorageFormat secureStorageFormat = Decrypt() ?? new SecureStorageFormat();
            switch (field)
            {
                case "email":
                    secureStorageFormat.Email = data;
                    break;
                case "password":
                    secureStorageFormat.Password = data;
                    break;
                case "token":
                    secureStorageFormat.Token = data;
                    break;
                default:
                    throw new ArgumentException("Invalid field name");
            }
            string jsonData = System.Text.Json.JsonSerializer.Serialize(secureStorageFormat);
            byte[] encryptedData = _Encrypt(jsonData);
            Directory.CreateDirectory("data");
            File.WriteAllBytes("data/secret.dat", encryptedData);
        }
        private static byte[] _Encrypt(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        }

        private static string _Decrypt(byte[] encryptedData)
        {
            byte[] decrypted = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
