using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicalLayer.Utilities
{
    public class HashGenerator
    {
        public static string ComputeSha256Hash(string password)
        {
            password = "Q33121a" + password + "gfdgf";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
