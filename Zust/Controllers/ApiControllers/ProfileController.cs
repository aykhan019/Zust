using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Web.Entities;
using Zust.Web.Helpers.ConstantHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [ApiController]
    [Route(Routes.ProfileController)]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the ProfileController class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        /// <param name="mapper">The mapper used for object mapping.</param>
        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;

            _mapper = mapper;
        }

        /// <summary>
        /// Updates the user profile with the provided data.
        /// </summary>
        /// <param name="updatedProfile">The updated user profile data.</param>
        /// <returns>The appropriate ActionResult based on the update result.</returns>
        [HttpPut(Routes.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(UserProfile updatedProfile)
        {
            // Retrieve the existing user profile from the database based on the ID
            var user = await _userService.GetUserByUsernameAsync(updatedProfile.UserName);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user profile with the new values
            _mapper.Map(updatedProfile, user);

            try
            {
                await _userService.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Return a 200 OK response indicating success
            return Ok();
        }
    }
}
