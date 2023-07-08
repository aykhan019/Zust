using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete;
using Zust.Entities.Models;
using Zust.Models;
using Zust.Web.Helpers.Constants;
using Zust.Web.Helpers.Validators;
using Zust.Web.Models;

namespace Zust.Web.Controllers
{

    /// <summary>
    /// Controller responsible for authentication-related actions, such as user registration and login.
    /// </summary>
    [Route(UrlConstants.Authentication)]
    [Controller]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Gets the configuration for accessing application settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Gets the manager for user sign-in functionality.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// The user manager component used for managing user-related operations.
        /// </summary>
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the AuthenticationController class with the required dependencies.
        /// </summary>
        /// <param name="authRepository">The repository for authentication-related operations.</param>
        /// <param name="configuration">The configuration for accessing application settings.</param>
        /// <param name="signInManager">The manager for user sign-in functionality.</param>
        public AuthenticationController(IConfiguration configuration,
                                        SignInManager<User> signInManager,
                                        UserManager<User> userManager,
                                        RoleManager<Role> roleManager,
                                        IUserService userService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

            // Create an instance of the custom password validator
            var noPasswordValidator = new NoPasswordValidator<User>();

            // Clear the existing password validators and add the custom validator
            _userManager.PasswordValidators.Clear();
            _userManager.PasswordValidators.Add(noPasswordValidator);
            _userService = userService;
        }

        [HttpPost(UrlConstants.Register)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _userService.UsernameIsTakenAsync(model.Username))
            {
                model.Errors.Add(ErrorConstants.UsernameIsTakenError);
                return RedirectToAction(UrlConstants.Error, UrlConstants.Home, model);
            }

            // Create a new User object and populate its properties
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            // Register the user with the provided password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the "User" role to the registered user
                if (!_roleManager.RoleExistsAsync(RoleConstants.User).Result)
                {
                    var role = new Role
                    {
                        Name = RoleConstants.User
                    };

                    await _roleManager.CreateAsync(role);
                }

                _userManager.AddToRoleAsync(user, RoleConstants.User).Wait();

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(UrlConstants.Index, UrlConstants.Home);
            }

            // Error
            var vm = new ErrorViewModel()
            {
                Error = ErrorConstants.RegisterError
            };
            return RedirectToAction(UrlConstants.Error, UrlConstants.Home, vm);
        }

        [HttpPost(UrlConstants.Login)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect to home/index with the generated token
                    return RedirectToAction(UrlConstants.Index, UrlConstants.Home);
                }
            }

            model.Errors.Add(ErrorConstants.InvalidLoginError);
                
            // If there are any validation errors, return the login form with the model
            return RedirectToAction(UrlConstants.Login, UrlConstants.Account, routeValues: model);
        }

        [HttpGet(UrlConstants.UserExistsRoute)]
        public async Task<IActionResult> UserExistsAsync(string username)
        {
            var userExists = await _userService.UsernameIsTakenAsync(username);
            return Ok(userExists);
        }
    }

}
