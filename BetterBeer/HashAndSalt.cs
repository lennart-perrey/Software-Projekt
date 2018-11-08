using System;
using System.Collections.Generic;
using System.Text;

namespace BetterBeer
{
    public static class HashAndSalt
    {
        private static string EncryptString(string input)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string CreateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public static string HashString(string str)
        {
            return EncryptString(str);
        }
    }
}

