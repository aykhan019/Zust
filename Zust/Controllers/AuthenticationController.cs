using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.Constants;
using Zust.Web.Models;

namespace Zust.Web.Controllers
{

    /// <summary>
    /// Controller responsible for authentication-related actions, such as user registration and login.
    /// </summary>
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Gets the repository for authentication-related operations.
        /// </summary>
        private readonly IAuthenticationRepository _authRepository;

        /// <summary>
        /// Gets the configuration for accessing application settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Gets the manager for user sign-in functionality.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Initializes a new instance of the AuthenticationController class with the required dependencies.
        /// </summary>
        /// <param name="authRepository">The repository for authentication-related operations.</param>
        /// <param name="configuration">The configuration for accessing application settings.</param>
        /// <param name="signInManager">The manager for user sign-in functionality.</param>
        public AuthenticationController(IAuthenticationRepository authRepository,
                                        IConfiguration configuration,
                                        SignInManager<User> signInManager)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost(UrlConstants.Register)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _authRepository.UserExists(model.Username))
            {
                ModelState.AddModelError(ErrorConstants.UsernameError, ErrorConstants.UsernameExistsError);
            }

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.GetErrorMessages();
                return RedirectToAction(UrlConstants.Register, UrlConstants.Account, model);
            }

            var userToCreate = new User()
            {   
                UserName = model.Username,
                Email = model.Email,
            };

            var userRegistered = await _authRepository.Register(userToCreate, model.Password);
            if (userRegistered != null)
            {
                // Sign the user in
                await _signInManager.SignInAsync(userToCreate, true);
                return RedirectToAction(UrlConstants.Login, UrlConstants.Account);
            }
            return RedirectToAction(UrlConstants.Login, UrlConstants.Account);
        }

        [HttpPost(UrlConstants.Login)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _authRepository.Login(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection(TokenConstants.TokenSection).Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    }),

                Expires = DateTime.Now.AddDays(TokenConstants.TokenExpiry),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            HttpContext.Session.SetString(TokenConstants.MyToken, tokenString);

            //await _signInManager.SignInAsync(user, dto.RememberMe);

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

            return RedirectToAction(UrlConstants.Index, UrlConstants.Home);
        }
    }

}
