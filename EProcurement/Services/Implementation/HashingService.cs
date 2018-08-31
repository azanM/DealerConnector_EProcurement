using EProcurement.Services.Interface;
using System.Security.Cryptography;
using System.Text;

namespace EProcurement.Services.Implementation
{
    public class HashingService : IHashingService
    {
        public string CreatePasswordHash(string password)
        {
            SHA1Managed sha1 = new SHA1Managed();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}