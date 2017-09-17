using System;

namespace Crypto1
{
    public class TestsHandler
    {
        private static readonly string fileForTestsGenerating = "Text.txt";
        private static readonly string keyForTestGenerating = "AndwhenIgetdrunkYoutakemehomeandkeepmesafefromharm".ToUpper();
        private static readonly string textForTestsGenerating;
        private static readonly string fileForAttackAnalysisTextLengthTestsResults = "AttackAnalysisTextLengthTests.txt";
        private static readonly string fileForAttackAnalysisKeyLengthTestsResults = "AttackAnalysisKeyLengthTests.txt";
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static int startPosition;
        private static string randomText;
        private static string randomKey;
        private static string encodedText;
        private static int guessedKeyWordLength;
        private static string guessedKeyWord;
        private static double match;
        private static double successGuess;
        private static double successGuessEps;
        private static double successGuessWhole;

        static TestsHandler()
        {
            textForTestsGenerating = FileHandler.ReadText(fileForTestsGenerating);
        }

        public static void AttackAnalysisKeyLength(int numberOfTests, double eps, int textLength, int lgramParameter)
        {
            int[] keyLengths =
            {
                5, 20, 30
            };

            FileHandler.WriteText("Starting test...", fileForAttackAnalysisKeyLengthTestsResults, false);
            foreach (var keyLength in keyLengths)
            {
                successGuess = 0;
                successGuessEps = 0;
                successGuessWhole = 0;

                RunTests(numberOfTests, textLength, keyLength, lgramParameter, eps, fileForAttackAnalysisTextLengthTestsResults);
            }
        }

        public static void AttackAnalysisTextLength(int numberOfTests, double eps, int keyLength, int lgramParameter)
        {
            int[] textLengths =
            {
                500, 1000, 1500
            };

            FileHandler.WriteText("Starting test...", fileForAttackAnalysisTextLengthTestsResults, false);
            foreach (var textLength in textLengths)
            {
                successGuess = 0;
                successGuessEps = 0;
                successGuessWhole = 0;

                RunTests(numberOfTests, textLength, keyLength, lgramParameter, eps, fileForAttackAnalysisKeyLengthTestsResults);
            }
        }

        private static double Match(string a, string b)
        {
            int n = a.Length;
            int sim = 0;

            for (int i = 0; i < n; i++)
            {
                sim += (a[i] == b[i] ? 1 : 0);
            }

            return (double)sim / n;
        }

        private static void RunTests(int numberOfTests, int textLength, int keyLength, int lgramParameter,
            double eps, string fileName)
        {
            for (int i = 1; i <= numberOfTests; i++)
            {
                startPosition = RandomNumber(textForTestsGenerating.Length - textLength - 1);
                randomText = textForTestsGenerating.Substring(startPosition, textLength);

                startPosition = RandomNumber(keyForTestGenerating.Length - keyLength);
                randomKey = keyForTestGenerating.Substring(startPosition, keyLength);

                FileHandler.WriteText("      Text:" + randomText, fileName, true);
                FileHandler.WriteText("      KeyWord:" + randomKey, fileName, true);

                encodedText = VigenereMethod.Encode(randomText, randomKey);

                guessedKeyWordLength = KasiskiMethod.GuessKeyWordLength(encodedText, lgramParameter);

                if (guessedKeyWordLength == keyLength)
                {
                    successGuess++;
                    guessedKeyWord = KasiskiMethod.GuessKey(encodedText, guessedKeyWordLength);

                    match = Match(randomKey, guessedKeyWord);
                    if (match >= eps)
                    {
                        successGuessEps++;
                    }
                    if (match == 1.0)
                    {
                        successGuessWhole++;
                    }

                    FileHandler.WriteText("      Guessed key word: " + guessedKeyWord, fileName, true);
                }
                else
                {
                    FileHandler.WriteText("      Guessed key word length: " + guessedKeyWordLength, fileName, true); ;
                }

                FileHandler.WriteText("******************************************************", fileName, true);
            }

            FileHandler.WriteText(" Summary: TextLength: " + textLength
                + " Guessed key word length: " + successGuess / numberOfTests
                + " Guessed key word (" + eps * 100 + "% match): " + successGuessEps / numberOfTests
                + " Guessed key word (100% match): " + successGuessWhole / numberOfTests, fileName, true);

            FileHandler.WriteText("----------------------------------------------------------", fileName, true);
        }

        private static int RandomNumber(int endPosition)
        {
            lock (syncLock)
            {
                return random.Next(0, endPosition);
            }
        }
    }
}
