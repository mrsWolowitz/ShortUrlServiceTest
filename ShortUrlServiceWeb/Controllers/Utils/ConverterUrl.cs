using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShortUrlServiceWeb.Controllers.Utils
{
    public class ConverterUrl
    {
        private int MinimalShortUrlLength;
        private string Alphabet;
        public string Label { get; }

        private const string ALPHABET = "123456789abcdefghijklmnopqrstuvwxyz";
        private const string LABEL = "0";
        private const int ALPHABET_MIN_LENGTH = 16;


        public ConverterUrl(int minimalShortUrlLength, string alphabet = ALPHABET, string label = LABEL)
        {
            if (alphabet.Contains(label))
                throw new ArgumentException($"Алфавит не должен содержать метку {label}", "alphabet");

            if (alphabet.Length < ALPHABET_MIN_LENGTH)
                throw new ArgumentException($"Алфавит должен быть не короче {ALPHABET_MIN_LENGTH} символов", "alphabet");

            this.MinimalShortUrlLength = minimalShortUrlLength;
            this.Alphabet = alphabet;
            this.Label = label;
        }

        public string Encode(long input)
        {
            StringBuilder ret = new StringBuilder();
            Random random = new Random();

            char[] alphabetArr = ALPHABET.ToCharArray();
            int alphabetLength = alphabetArr.Length;
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(alphabetArr[input % alphabetLength]);
                input /= alphabetLength;
            }
            ret.Append(new string(result.ToArray()));

            if (ret.Length < MinimalShortUrlLength)
            {
                ret.Append(LABEL);
                while (ret.Length < MinimalShortUrlLength)
                {
                    int randomIndex = random.Next(alphabetLength);
                    ret.Append(alphabetArr[randomIndex]);
                }
            }
            return ret.ToString();
        }

        public long Decode(string input)
        {
            string valueInput = _GetValuePartOfString(input);

            int alphabetLength = ALPHABET.Length;
            var reversed = valueInput.Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += ALPHABET.IndexOf(c) * (long)Math.Pow(alphabetLength, pos);
                pos++;
            }
            return result;
        }

        private string _GetValuePartOfString(string str)
        {
            int valueLength = str.IndexOf(Label);
            if (valueLength > 0)
                str = str.Substring(0, valueLength);
            return str;
        }
    }
}
