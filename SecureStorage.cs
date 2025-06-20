using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace r6marketplaceclient
{
    internal class SecureStorageFormat
    {
        public string? email { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }
    }
    internal class SecureStorage
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
            SecureStorageFormat secureStorageFormat = new SecureStorageFormat
            {
                email = email,
                password = password,
                token = token
            };
            string jsonData = System.Text.Json.JsonSerializer.Serialize(secureStorageFormat);
            byte[] encryptedData = _Encrypt(jsonData);
            Directory.CreateDirectory("data");
            File.WriteAllBytes("data/secret.dat", encryptedData);
        }
        internal static void Encrypt(string field, string data)
        {
            SecureStorageFormat secureStorageFormat = Decrypt() ?? new SecureStorageFormat();
            switch (field)
            {
                case "email":
                    secureStorageFormat.email = data;
                    break;
                case "password":
                    secureStorageFormat.password = data;
                    break;
                case "token":
                    secureStorageFormat.token = data;
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
