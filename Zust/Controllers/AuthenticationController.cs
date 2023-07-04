using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;
using Zust.Helpers;
using Zust.Web.Helpers;
using Zust.Web.Models;

namespace Zust.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepository _authRepository;
        private IConfiguration _configuration;

        public AuthenticationController(IAuthenticationRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost(Constants.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _authRepository.UserExists(model.Username))
            {
                ModelState.AddModelError(ErrorConstants.UsernameErrorName, ErrorConstants.UsernameErrorText);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToCreate = new User()
            {
                UserName = model.Username,
            };

            await _authRepository.Register(userToCreate, model.Password);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost(Constants.Login)]
        public async Task<IActionResult> Login([FromBody] LoginModel dto)
        {
            var user = await _authRepository.Login(dto.Username, dto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection(Constants.TokenSection).Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity( 
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    }),
    
                    Expires = DateTime.Now.AddDays(1),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }

}
