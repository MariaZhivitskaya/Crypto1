using System.Text;

namespace Crypto1
{
    public class VigenereMethod
        : Encoder
    {
        public static string Encode(string input, string key)
        {
            return Encode(input, key, 1);
        }

        public static string Decode(string input, string key)
        {
            return Encode(input, key, -1);
        }

        private static string Encode(string input, string key, int encode)
        {
            StringBuilder sb = new StringBuilder(input.ToUpper());

            for (int i = 0, j = 0; i < sb.Length; i++)
            {
                j = (j + 1) % key.Length;
                sb[i] = EncodeCharacter(sb[i], encode * (key[j] - 'A'));
            }

            return sb.ToString();
        }
    }
}
