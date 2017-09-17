namespace Crypto1
{
    public class Encoder
    {
        public const int Capacity = 26;

        public static char EncodeCharacter(char character, int position)
        {
            char encodedCharacter = character;
            position %= Capacity;

            if (character >= 'A' && character <= 'Z')
            {
                encodedCharacter = (char)((encodedCharacter - 'A' + position + Capacity) % Capacity + 'A');
            }

            return encodedCharacter;
        }
    }
}