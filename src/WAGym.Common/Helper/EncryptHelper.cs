using System.Security.Cryptography;
using System.Text;

namespace WAGym.Common.Helper
{
    public static class EncryptHelper
    {
        public static string Sha512(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using(SHA512 hash = SHA512.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(bytes);
                StringBuilder sbHashedText = new StringBuilder(128);

                foreach (byte b in hashedBytes)
                    sbHashedText.Append(b.ToString());

                return sbHashedText.ToString();
            }
        }
    }
}
