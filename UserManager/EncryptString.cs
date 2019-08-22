using Repository1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Manager.UserManager
{
    public class EncryptString : AsistanceBase<UserAdapter, User>
    {
        public EncryptString(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public string ValidateHashMD5(string usr)
        {
            string username = "muhmmdfrd";

            using (MD5 md5Hash = MD5.Create())
            {
                var hashUsr = GetMd5Hash(md5Hash, usr);
                var resultVerify = (VerifyMd5Hash(md5Hash, username, hashUsr)) ? "your account is valid" : "your account not valid";

                return resultVerify;
            }
        }

        public string Encrypt(string pass)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, pass);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            bool isVerifyMd5 = (0 == comparer.Compare(hashOfInput, hash));

            return isVerifyMd5;
        }


    }
}
