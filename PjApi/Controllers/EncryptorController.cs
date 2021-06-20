using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace PjApi.Controllers
{
    public class EncryptorController : ApiController
    {
        public static class StringCipher
        {
            public static string cypher_str = "Soft.3521";
            private const int Keysize = 256;
            private const int DerivationIterations = 1000;

            public static string Encrypt(string encryptString)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(encryptString);
                using (Aes aes = Aes.Create())
                {
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(cypher_str, new byte[13]
                    {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                    });
                    aes.Key = rfc2898DeriveBytes.GetBytes(32);
                    aes.IV = rfc2898DeriveBytes.GetBytes(16);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes, 0, bytes.Length);
                            cryptoStream.Close();
                        }
                        encryptString = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
                return encryptString;
            }

            public static string Decrypt(string cipherText)
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(cypher_str, new byte[13]
                    {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                    });
                    aes.Key = rfc2898DeriveBytes.GetBytes(32);
                    aes.IV = rfc2898DeriveBytes.GetBytes(16);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(buffer, 0, buffer.Length);
                            cryptoStream.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
                return cipherText;
            }
        }
    }
}
