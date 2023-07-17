using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(Routes.Post)]
    [Controller]
    public class PostController : ControllerBase
    {
        [HttpPost("CreatePost")]
        public async Task CreatePost([FromForm] CreatePostViewModel model)
        {
            IFormFile mediaFile = model.MediaFile;
        }
    }
}
