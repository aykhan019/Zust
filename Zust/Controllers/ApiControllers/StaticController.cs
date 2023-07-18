using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.ImageHelpers;

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


    }
}
