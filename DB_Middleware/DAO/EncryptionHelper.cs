using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB_Middleware.DAO
{
    public class EncryptionHelper
    {

        private static readonly string Key = "+miX/CtjYFXDQ4pSDTOhfaQzTMGdMacWMADcN+RlNj8=";
        private static readonly string IV = "HX+cSK7P00B2nm15FgpVCw==";



        public static string DBEncrypt(string plainText)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        // Decrypt string
        public static string DBDecrypt(string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText)) return "";

            try
            {

                byte[] data = Convert.FromBase64String(encryptedText);
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch (FormatException)
            {
                throw new Exception("The input is not a valid Base64 string.");
            }

        }


        public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        byte[] encryptedBytes;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(Key); // 32 bytes
            aes.IV = Convert.FromBase64String(IV);   // 16 bytes
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var encryptor = aes.CreateEncryptor())
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                } // <-- StreamWriter & CryptoStream flushed/closed here

                encryptedBytes = ms.ToArray(); // now contains encrypted data
            }
        }

        return Convert.ToBase64String(encryptedBytes);
    }

    // Decrypt ciphertext
    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(Key); // must match Encrypt
            aes.IV = Convert.FromBase64String(IV);   // must match Encrypt
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor())
            using (var ms = new MemoryStream(cipherBytes))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

    }
}
