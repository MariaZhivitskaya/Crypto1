using System.IO;

namespace Crypto1
{
    public class FileHandler
    {
        public static string ReadText(string filename)
        {
            using (StreamReader streamReader = new StreamReader(filename))
            {
                return streamReader.ReadToEnd().ToUpper();
            }
        }

        public static void WriteText(string text, string filename, bool leaveOpen)
        {
            using (StreamWriter streamWriter = new StreamWriter(filename, leaveOpen))
            {
                streamWriter.WriteLine(text);
            }
        }
    }
}