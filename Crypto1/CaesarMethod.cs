using System.Text;

namespace Crypto1
{
    public class CaesarMethod
        : Encoder
    {
        public static string Encode(string input, int position)
        {
            StringBuilder sb = new StringBuilder(input.ToUpper());

            for (int i = 0; i < sb.Length; i++)
            {
                sb[i] = EncodeCharacter(sb[i], position);
            }

            return sb.ToString();
        }

        public static string Decode(string input, int position)
        {
            return Encode(input, -position);
        }
    }
}
