using Microsoft.AspNetCore.Mvc;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.Static)]
    [ApiController]
    public class StaticController : ControllerBase
    {
        private readonly IStaticService _staticService;

        public StaticController(IStaticService staticService)
        {
            _staticService = staticService;
        }

        [HttpGet(Routes.GetRandomStatusImagePaths)]
        public ActionResult<List<string>> GetRandomStatusImagePaths()
        {
            try
            {
                string path = Path.Combine(FileConstants.FilesFolderPath, FileConstants.StatusImagesFile);

                var paths = _staticService.GetRandomStatusImagePaths(Constants.StatusCountInNewsFeed, path);

                return Ok(paths);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet(Routes.GetWatchVideos)]
        public ActionResult<List<string>> GetWatchVideos()
        {
            try
            {
                string path = Path.Combine(FileConstants.FilesFolderPath, FileConstants.WatchVideoUrlsFile);

                var videos = _staticService.GetWatchVideos(path);

                return Ok(videos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetSpecialUsers)]
        public async Task<ActionResult<List<User>>> GetSpecialUsers()
        {
            try
            {
                var specialUsers = await _staticService.GetSpecialUsersAsync();

                return Ok(specialUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
