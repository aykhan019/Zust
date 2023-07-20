using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
