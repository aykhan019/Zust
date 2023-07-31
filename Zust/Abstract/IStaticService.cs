using Zust.Entities.Models;
using Zust.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zust.Web.Abstract
{
    /// <summary>
    /// Interface for managing static content-related operations, such as retrieving random cover images, status image paths, watch videos, advertisements, and special users.
    /// </summary>
    public interface IStaticService
    {
        /// <summary>
        /// Retrieves a random cover image URL from the specified path.
        /// </summary>
        /// <param name="path">The file path containing the list of cover image URLs.</param>
        /// <returns>A random cover image URL.</returns>
        string GetRandomCoverImage(string path);

        /// <summary>
        /// Retrieves a list of random status image paths from the specified path.
        /// </summary>
        /// <param name="count">The number of random status image paths to retrieve.</param>
        /// <param name="path">The file path containing the list of status image URLs.</param>
        /// <returns>A list of random status image paths.</returns>
        List<string> GetRandomStatusImagePaths(int count, string path);

        /// <summary>
        /// Retrieves a list of watch videos from the specified path.
        /// </summary>
        /// <param name="path">The file path containing the list of watch videos.</param>
        /// <returns>A list of watch videos.</returns>
        List<Video> GetWatchVideos(string path);

        /// <summary>
        /// Retrieves a list of advertisements from the specified path.
        /// </summary>
        /// <param name="path">The file path containing the list of advertisements.</param>
        /// <returns>A list of advertisements.</returns>
        List<Advertisement> GetAdvertisements(string path);

        /// <summary>
        /// Retrieves a list of special users asynchronously.
        /// </summary>
        /// <returns>A list of special users.</returns>
        Task<List<User>> GetSpecialUsersAsync();
    }
}