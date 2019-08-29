﻿using System;
using System.Linq;

namespace Utils
{
    public class Generate
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        private static readonly Random _random = new Random();

        public string RandomSalt()
        {
            return RandomString(Chars, 64, 64);
        }

        public string RandomName()
        {
            var name = RandomString(Alphabet, 5, 20);

            return $"{Char.ToUpper(name[0])}{name.Substring(1)}";
        }

        public string RandomEmail()
        {
            return $"{RandomString(Chars, 5, 20)}@{RandomString(Alphabet, 3, 10)}.{RandomString(Alphabet, 2, 5)}";
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
