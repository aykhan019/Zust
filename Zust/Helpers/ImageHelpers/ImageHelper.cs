using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.FileHelpers;

namespace Zust.Web.Helpers.ImageHelpers
{
    /// <summary>
    /// Helper class for working with images.
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// Retrieves a random cover image URL.
        /// </summary>
        /// <returns>A randomly selected cover image URL.</returns>
        public static string GetRandomCoverImage()
        {
            var path = Path.Combine(Constants.FilesFolderPath, Constants.CoversFile);

            var imageUrls = FileHelper.GetCoverImagesFromFile(path);

            var rand = new Random().Next(0, imageUrls.Count);

            return imageUrls.ElementAt(rand);
        }
    }
}