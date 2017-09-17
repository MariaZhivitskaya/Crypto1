using System;

namespace Crypto1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            string encodedText;
            int position = 3;
            int lgramParameter = 4;
            int numberOfTests = 30;
            double eps = 0.2;
            int textLength = 1000;
            int keyLength = 8;

            string fileInput = "Text.txt";
            string fileOutputCaesarEncode = "CaesarEncode.txt";
            string fileOutputCaesarDecode = "CaesarDecode.txt";
            string fileOutputVigenereEncode = "VigenereEncode.txt";
            string fileOutputVigenereDecode = "VigenereDecode.txt";

            text = FileHandler.ReadText(fileInput);

            encodedText = CaesarMethod.Encode(text, position);
            FileHandler.WriteText(encodedText, fileOutputCaesarEncode, false);
            FileHandler.WriteText(CaesarMethod.Decode(encodedText, position), fileOutputCaesarDecode, false);

            string key = "supernatural".ToUpper();
            encodedText = VigenereMethod.Encode(text, key);
            FileHandler.WriteText(encodedText, fileOutputVigenereEncode, false);
            FileHandler.WriteText(VigenereMethod.Decode(encodedText, key), fileOutputVigenereDecode, false);

            int guessedKeyLength = KasiskiMethod.GuessKeyWordLength(encodedText, lgramParameter);
            FileHandler.WriteText("----------------------------------------------------------",
                fileOutputVigenereDecode, true);
            FileHandler.WriteText("Guessed key word length: " + guessedKeyLength, fileOutputVigenereDecode, true);

            TestsHandler.AttackAnalysisTextLength(numberOfTests, eps, textLength, lgramParameter);
            TestsHandler.AttackAnalysisKeyLength(numberOfTests, eps, keyLength, lgramParameter);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
