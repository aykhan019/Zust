namespace Zust.Web.Abstract
{
    /// <summary>
    /// Interface for managing media-related operations, such as uploading media files and determining if a file is a video.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Uploads a media file asynchronously to the cloud storage service.
        /// </summary>
        /// <param name="file">The media file to be uploaded.</param>
        /// <returns>The URL of the uploaded media file.</returns>
        Task<string> UploadMediaAsync(IFormFile file);

        /// <summary>
        /// Determines if a media file is a video.
        /// </summary>
        /// <param name="mediaFile">The media file to check.</param>
        /// <returns>True if the file is a video, false otherwise.</returns>
        bool IsVideoFile(IFormFile mediaFile);
    }
}