using Newtonsoft.Json;

namespace Zust.Web.Helpers.FileHelpers
{
    /// <summary>
    /// Helper class for working with files.
    /// </summary>
    public class FileHelper<T> where T: class
    {
        /// <summary>
        /// Retrieves a list of images from a text file.
        /// </summary>
        /// <param name="path">The path to the text file.</param>
        /// <returns>A list of images.</returns>
        public static List<string> ReadTextFile(string path)
        {
            var lines = File.ReadAllLines(path);

            return lines.ToList();
        }

        public static void Serialize(List<T> values, string filename)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(filename))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, values);
                }
            }
        }

        public static List<T?> Deserialize(string filename)
        {
            List<T> values = new List<T>();
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(filename))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    values = serializer.Deserialize<List<T>>(jr);
                }
            }
            return values;
        }

    }
}