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
    //[Produces("application/json")]
    //[Route("api/authentication")]
    //[ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;


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
                return BadRequest(ModelState);

            var userToCreate = new User()
            {
                UserName = model.Username,
                Email = model.Email,
            };

            await _authRepository.Register(userToCreate, model.Password);

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(UrlConstants.Home);
                }
                ModelState.AddModelError(ErrorConstants.LoginError, ErrorConstants.InvalidLoginError);
            }

            return RedirectToAction(UrlConstants.Login, UrlConstants.Account);
        }

        [HttpPost(UrlConstants.Login)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel dto)
        {
            var user = await _authRepository.Login(dto.Username, dto.Password);

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

            await _signInManager.SignInAsync(user, dto.RememberMe);

            return RedirectToAction(UrlConstants.Index, UrlConstants.Home);
        }
    }

}
