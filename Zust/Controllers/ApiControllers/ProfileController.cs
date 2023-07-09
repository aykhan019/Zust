﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Entities;
using Zust.Web.Helpers.Constants;

namespace Zust.Web.Controllers.ApiControllers
{
    [ApiController]
    [Route(UrlConstants.ProfileController)]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        // PUT: api/profile/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserProfile updatedProfile)
        {
            // Retrieve the existing user profile from the database based on the ID
            var user = await _userService.GetUserByUsernameAsync(updatedProfile.UserName);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user profile with the new values
            user = _mapper.Map<User>(updatedProfile);

            await _userService.UpdateAsync(user);

            return Ok(); // Return a 200 OK response indicating success
        }
    }
}
