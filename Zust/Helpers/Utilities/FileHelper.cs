using Newtonsoft.Json;

namespace Zust.Web.Helpers.Utilities
{
    /// <summary>
    /// Helper class for working with files.
    /// </summary>
    public class FileHelper<T> where T : class
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

        /// <summary>
        /// Serializes a list of objects to JSON format and writes it to a file with the specified filename.
        /// </summary>
        /// <param name="values">The list of objects to be serialized.</param>
        /// <param name="filename">The filename for the output JSON file.</param>
        public static void Serialize(List<T> values, string filename)
        {
            var serializer = new JsonSerializer();

            using var sw = new StreamWriter(filename);

            using var jw = new JsonTextWriter(sw);

            jw.Formatting = Formatting.Indented;

            serializer.Serialize(jw, values);
        }

        /// <summary>
        /// Deserializes JSON data from a file with the specified filename and converts it back to a list of objects.
        /// </summary>
        /// <param name="filename">The filename of the JSON file to be deserialized.</param>
        /// <returns>The deserialized list of objects.</returns>
        public static List<T?> Deserialize(string filename)
        {
            List<T> values = new();

            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(filename))
            {
                using var jr = new JsonTextReader(sr);

                values = serializer.Deserialize<List<T>>(jr);
            }

            return values;
        }
    }
}