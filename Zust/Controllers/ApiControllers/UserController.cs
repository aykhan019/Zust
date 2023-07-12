using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.UserHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    [Route(UrlConstants.UserController)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
    
        public UserController(IUserService userService)
        {
            _userService = userService;
           
        }

        [HttpGet(UrlConstants.GetAllUsersCount)]
        public async Task<ActionResult<int>> GetAllUsersCount()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(UrlConstants.GetUsers)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int startIndex, int userCount)
        {
            try
            {
                var serviceProvider = HttpContext.RequestServices;
                var users = await _userService.GetAllUsersAsync();
                var list = users.ToList();
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);
                // Excluded the current user to avoid displaying it among Zust Users, as the current user is the one viewing the user list.
                list.RemoveAll(u => u.Id == currentUser.Id);
                var range =new Range(startIndex, startIndex + userCount);
                return Ok(list.Take(range));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(UrlConstants.GetUser)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(string id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
