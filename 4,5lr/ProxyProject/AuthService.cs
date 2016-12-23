using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProxyProject
{
    public class AuthService
    {
        public static string GetSaltPassword(int password)
        {
            string saltPassword = "";
            Encoding enc = Encoding.Unicode;
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            byte[] passwordByte = BitConverter.GetBytes(password);
            byte[] hashPassword = sha1.ComputeHash(passwordByte);
            byte[] salt = File.ReadAllBytes("salt.txt");
            saltPassword = enc.GetString(hashPassword) + enc.GetString(salt);
            return saltPassword;
        }
        public static string[] ReadPassword()
        {
            return File.ReadAllLines("./password.txt");
        }
        private static void GenSalt()
        {
            int LengthSalt = 20;
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[LengthSalt];
            rngCsp.GetBytes(salt);
            File.WriteAllBytes("salt.txt", salt);
        }
        public static void RetainClients(int CountClient)
        {
            GenSalt();
            StreamWriter file = new StreamWriter("password.txt");
            for (int i = 0; i < CountClient; i++)
            {
                string saltPassword = "";
                saltPassword = GetSaltPassword(i);
                file.WriteLine(saltPassword);
            }
            file.Close();
        }
        public static bool CheckClient (Client client)
        {
            bool flag = false;
            string[] passwords = ReadPassword();
            string passwordThisClient = GetSaltPassword(client.Id);
            for (int i = 0; (i < passwords.Length) && (!flag); i++)
            {
                if (passwordThisClient == passwords[i])
                {
                    flag = true;
                }
            }
            return flag;
        }
        public static void AuthClient(Client client)
        {
            if (!CheckClient(client))
            {
                throw new AuthException();
            }
        }
    }
}
