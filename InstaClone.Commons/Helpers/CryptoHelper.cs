using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Helpers
{
    public static class CryptoHelper
    {
        /// <summary>
        /// Usado para convertir un texto a hash MD5
        /// </summary>
        /// <param name="word">Texto a convertir</param>
        /// <returns>Devuelve un string MD5</returns>
        public static string HashMD5(string word)
        {
            string hashString = string.Empty;
            using (MD5 mD5 = MD5.Create())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(word);
                var hash = mD5.ComputeHash(bytes);

                foreach (byte x in hash)
                {
                    hashString += String.Format("{0:x2}", x);
                }
            }
            return hashString;
        }
    }
}
