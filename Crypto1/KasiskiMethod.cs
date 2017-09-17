using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto1
{
    public class KasiskiMethod
    {
        public static int GuessKeyWordLength(string input, int lgramParameter)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string lgram;
            bool consistOnlyCharacters;
            int index;
            int count;

            for (int i = 0; i < input.Length - lgramParameter + 1; i++)
            {
                lgram = input.Substring(i, i + lgramParameter - i);

                consistOnlyCharacters = true;
                foreach (char character in lgram)
                {
                    if (!(character >= 'A' && character <= 'Z'))
                    {
                        consistOnlyCharacters = false;
                    }
                }

                if (!consistOnlyCharacters)
                {
                    continue;
                }

                if (dict.ContainsKey(lgram))
                {
                    continue;
                }

                index = i;
                count = 1;

                while ((index = input.IndexOf(lgram, index + 1, StringComparison.Ordinal)) != -1)
                {
                    count++;
                }

                if (count > 2)
                {
                    dict.Add(lgram, count);
                }
            }

            List<KeyValuePair<string, int>> list =
                new List<KeyValuePair<string, int>>(dict.Select(x => new KeyValuePair<string, int>(x.Key, x.Value)));

            list.Sort((x, y) => x.Value - y.Value);

            int prevIndex;
            int difference;
            int res = -1;

            for (int i = 0; i < 10 && i < list.Count(); i++)
            {
                lgram = list[i].Key;
                prevIndex = input.IndexOf(lgram, StringComparison.Ordinal);
                index = input.IndexOf(lgram, prevIndex + 1, StringComparison.Ordinal);
                difference = index - prevIndex;

                for (; index != -1; prevIndex = index,
                    index = input.IndexOf(lgram, index + 1, StringComparison.Ordinal))
                {

                    difference = GreatestCommonDivisor(difference, index - prevIndex);
                }

                res = difference;

                if (res > 2)
                {
                    break;
                }
            }

            return res;
        }

        public static string GuessKey(string input, int keyWordLength)
        {
            StringBuilder keyWord = new StringBuilder();
            string substr;

            for (int i = 0; i < keyWordLength; i++)
            {
                substr = GetSubstring(input, keyWordLength, i);
                keyWord.Append((char)(GuessOffset(substr) + 'A'));
            }

            return keyWord.ToString();
        }

        private static string GetSubstring(string input, int symbolPosition, int startPosition)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = startPosition; i < input.Length; i += symbolPosition)
            {
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    sb.Append(input[i]);
                }
            }

            return sb.ToString();
        }

        private static int GuessOffset(string str)
        {
            double[] m = CalculateFrequencies(str);
            double max = 0.0;
            int maxIndex = -1;

            for (int i = 0; i < Encoder.Capacity; i++)
            {
                if (m[i] > max)
                {
                    maxIndex = i;
                    max = m[i];
                }
            }

            int length = ('E' - 'A') - maxIndex;

            return (-length + Encoder.Capacity) % Encoder.Capacity;
        }

        private static double[] CalculateFrequencies(string str)
        {
            double[] frequencies = new double[Encoder.Capacity];
            int n = 0;

            foreach (char character in str)
            {
                if (character >= 'A' && character <= 'Z')
                {
                    n++;
                    frequencies[character - 'A']++;
                }
            }

            for (int i = 0; i < Encoder.Capacity; i++)
            {
                frequencies[i] /= n;
            }

            return frequencies;
        }

        private static int GreatestCommonDivisor(int a, int b)
        {
            int tmp;

            if (a < b)
            {
                tmp = a;
                a = b;
                b = tmp;
            }

            while (a != 0 && b != 0)
            {
                a = a % b;

                if (a < b)
                {
                    tmp = a;
                    a = b;
                    b = tmp;
                }
            }

            return a;
        }
    }
}
