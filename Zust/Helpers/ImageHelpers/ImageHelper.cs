using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.FileHelpers;

namespace Zust.Web.Helpers.ImageHelpers
{
    public class ImageHelper
    {
        public static string GetRandomCoverImage()
        {
            var path = Path.Combine(Constants.FilesFolderPath, Constants.CoversFile);
            var imageUrls = FileHelper.GetCoverImagesFromFile(path);
            var rand = new Random().Next(0, imageUrls.Count);
            return imageUrls.ElementAt(rand);
        }
    }
}
