using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.ImageHelpers;
using Zust.Web.Entities;

namespace Zust.Web.Concrete
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration _configuration;
        private CloudinarySettings? _cloudinarySettings;
        private Cloudinary _cloudinary;

        public MediaService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinarySettings = _configuration.GetSection(Constants.CloudinarySettings)
                                                .Get<CloudinarySettings>();
            Account account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadMediaAsync(IFormFile file)
        {
            // Check the file type
            var fileType = file.ContentType;

            // Set the upload parameters based on the file type
            RawUploadParams uploadParams;

            if (fileType.StartsWith(Constants.ImageFileType))
            {
                uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
            }
            else if (fileType.StartsWith(Constants.VideoFileType))
            {
                uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
            }
            else
            {
                throw new NotSupportedException(Errors.FileTypeNotSupportedError);
            }

            // Upload the file to Cloudinary
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        public bool IsVideoFile(IFormFile mediaFile)
        {
            if (mediaFile.ContentType.StartsWith(Constants.VideoFileType))
            {
                return true;
            }
            return false;
        }
    }
}
