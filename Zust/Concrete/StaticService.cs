using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.FileHelpers;
using Zust.Web.Helpers.ImageHelpers;

namespace Zust.Web.Concrete
{
    public class StaticService : IStaticService
    {
        public string GetRandomCoverImage(string path)
        {
            var imageUrls = FileHelper.GetImagePathsFromFile(path);

            var rand = new Random().Next(0, imageUrls.Count);

            return imageUrls.ElementAt(rand);
        }

        public List<string> GetRandomStatusImagePaths(int count, string path)
        {
            var imageUrls = FileHelper.GetImagePathsFromFile(path);

            var rand = new Random();

            var randomImages = new List<string>();

            count = Math.Min(count, imageUrls.Count);

            while (randomImages.Count < count)
            {
                var randomIndex = rand.Next(0, imageUrls.Count);

                var randomImage = imageUrls[randomIndex];

                if (!randomImages.Contains(randomImage))
                {
                    randomImages.Add(randomImage);
                }
            }

            return randomImages;
        }
    }
}
