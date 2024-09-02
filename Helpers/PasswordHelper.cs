using System;
using System.Security.Cryptography;
using System.Text;

namespace PersonalFinanceApp.Helpers
{
    public static class PasswordHelper
    {
        public static string GetPassword()
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                password.Append(keyInfo.KeyChar);
                Console.Write("*");
            }
            return password.ToString();
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hash = HashPassword(password);
            return hash == hashedPassword;
        }
    }
}
