namespace Zust.Web.Helpers.FileHelpers
{
    /// <summary>
    /// Helper class for working with files.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Retrieves a list of cover images from a text file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        /// <returns>A list of cover images.</returns>
        public static List<string> GetCoverImagesFromFile(string path)
        {
            var lines = File.ReadAllLines(path);

            return lines.ToList();
        }
    }
}