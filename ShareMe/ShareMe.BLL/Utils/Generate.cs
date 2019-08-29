using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.BLL.Utils
{
    public class Generate
    {
        private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        private static readonly Random _random = new Random();

        public string RandomSalt()
        {
            return RandomString(Chars, 64, 64);
        }

        public string RandomString(string source, int minLength, int maxLength)
        {
            return new string(Enumerable
                .Repeat(source, _random.Next(minLength, maxLength))
                .Select(s => s[_random.Next(s.Length)])
                .ToArray()
            );
        }
    }
}
