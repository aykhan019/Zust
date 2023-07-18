using Zust.Web.Abstract;
using Zust.Web.Entities;
using Zust.Web.Extensions;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;

namespace Zust.Web.Concrete
{
    public class StaticService : IStaticService
    {
        public List<Advertisement?> GetAdvertisements(string path)
        {
            var advertisements = FileHelper<Advertisement>.Deserialize(path);

            advertisements.Shuffle();

            return advertisements.Take(Constants.AdvertisementCountInNewsFeed).ToList();
        }

        public string GetRandomCoverImage(string path)
        {
            var imageUrls = FileHelper<string>.ReadTextFile(path);

            var rand = new Random().Next(0, imageUrls.Count);

            return imageUrls.ElementAt(rand);
        }

        public List<string> GetRandomStatusImagePaths(int count, string path)
        {
            var imageUrls = FileHelper<string>.ReadTextFile(path);

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

        public List<Video?> GetWatchVideos(string path)
        {
            var videos = FileHelper<Video>.Deserialize(path).Take(Constants.VideoCountInNewsFeed).ToList();

            videos.Shuffle();

            return videos;
        }
    }
}
