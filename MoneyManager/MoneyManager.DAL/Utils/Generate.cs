using System;
using System.Linq;

namespace MoneyManager.DAL.Utils
{
    public static class Generate
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        private static readonly Random _random = new Random();

        public static string RandomSalt()
        {
            return RandomString(Chars, 64, 64);
        }

        public static string RandomName()
        {
            var name = RandomString(Alphabet, 5, 20);

            return $"{Char.ToUpper(name[0])}{name.Substring(1)}";
        }

        public static string RandomEmail()
        {
            return $"{RandomString(Chars, 5, 20)}@{RandomString(Alphabet, 3, 10)}.{RandomString(Alphabet, 2, 5)}";
        }

        public static string RandomString(string source, int minLength, int maxLength)
        {
            return new string(Enumerable
                .Repeat(source, _random.Next(minLength, maxLength))
                .Select(s => s[_random.Next(s.Length)])
                .ToArray()
            );
        }
    }
}
