using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        // injecting the newy created auth repository to this controller.
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExist(userForRegisterDto.Username))
            {
                return BadRequest("Username already exists.");
            }
            // creating the user instance with the passing user name.
            var userToCrate = new User
            {

                Username = userForRegisterDto.Username

            };
            var createdUser = await _repo.Register(userToCrate, userForRegisterDto.Password);

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            
            
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
            {
                return Unauthorized();

            }
            // creating token information with claims
            var claims = new[]{
              new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()) ,
              new Claim(ClaimTypes.Name,userFromRepo.Username)
            };

            // key to sign our token

            var key = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            // creating the signing credentials for token
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            // now create the security token descriptor. Which includes all above.

            var tokenDescriptor = new SecurityTokenDescriptor{
                  Subject = new ClaimsIdentity(claims),
                  Expires = DateTime.Now.AddDays(1),
                  SigningCredentials = creds

            };

            // creating the token handler

            var tokenHandler = new JwtSecurityTokenHandler();
            // creating the token using token decriptor which has the information.

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // returning our token to the client as an object.

            return Ok(new {
            token = tokenHandler.WriteToken(token)

            });

        }

    }
}