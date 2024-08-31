using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Adidy.Utils
{
    public class PasswordEncryption
    {
        //Encrypt the password
        public static string StrToShA1(string password)
        {
            if (!password.IsNullOrEmpty())
            {
                byte[] sourceByte = Encoding.UTF8.GetBytes(password);
                byte[] hashbyte = SHA1.HashData(sourceByte);
                string hash = BitConverter.ToString(hashbyte).Replace("-", String.Empty);
                return hash;
            }
            else
                throw new Exception("Password can't be empty");
        }
    }
}
