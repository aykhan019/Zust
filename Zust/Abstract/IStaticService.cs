using Zust.Entities.Models;
using Zust.Web.Entities;

namespace Zust.Web.Abstract
{
    public interface IStaticService
    {
        string GetRandomCoverImage(string path);
        List<string> GetRandomStatusImagePaths(int count, string path);
        List<Video> GetWatchVideos(string path);
        List<Advertisement> GetAdvertisements(string path);
        Task<List<User>> GetSpecialUsersAsync();
    }
}
