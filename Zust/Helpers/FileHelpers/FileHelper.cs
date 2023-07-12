namespace Zust.Web.Helpers.FileHelpers
{
    public class FileHelper
    {
        public static List<string> GetCoverImagesFromFile(string path)
        {
            // Read all lines from the text file
            var lines = File.ReadAllLines(path);

            return lines.ToList();
        }
    }
}
