using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Models;
using USER_SERVICE.Data;
using USER_SERVICE.Services;
using USER_SERVICE.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace USER_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        
        public UserController(UserService userService, IConfiguration configuration)
        {
            _userService=userService;
            _configuration=configuration;
        }

        [HttpPost("registrar estudiante")]
        public async Task<ActionResult<User>> Register(EstudianteDto dto)
        {
            try
            {
                var user = await _userService.RegistrarEstudiante(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("registrar docente")]
        public async Task<ActionResult<User>> Register(DocenteDto dto)
        {
            try
            {
                var user = await _userService.RegistrarDocente(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(DocenteLoginDto dto)
        {
            try
            {
                var user = await _userService.Login(dto);

                // Generate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.UUID) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{UUID}")]
        public async Task<ActionResult<User>> GetUser(string UUID)
        {
            var user = await _userService.GetUserbyGUID(UUID);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("List")]
        public async Task<ActionResult<User>> GetList()
        {
            return Ok();
        }
    }
}