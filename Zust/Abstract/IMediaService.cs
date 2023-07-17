namespace Zust.Web.Abstract
{
    public interface IMediaService
    {
        Task<string> UploadMediaAsync(IFormFile file);
    }
}
