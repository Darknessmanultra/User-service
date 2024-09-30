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
using Microsoft.AspNetCore.Authorization;

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


        [HttpGet("{UUID}")]
        public async Task<ActionResult<User>> GetUser(string UUID)
        {
            var user = await _userService.GetUserbyGUID(UUID);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("Lista de estudiantes")]
        public async Task<ActionResult<User>> GetoceList()
        {
            var user = await _userService.GetEstudiantesAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("Lista de docentes")]
        public async Task<ActionResult<User>> GetDocenteList()
        {
            var user = await _userService.GetDocentesAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("Edit Student")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult<Estudiante>> EditarEstudiante(EstudianteDto dto, string UUID)
        {
            var user= await _userService.GetEstudiantebyGUID(UUID);
            if(user==null) return NotFound();
            await _userService.EditStudent(dto,UUID);
            return Ok(user);
        }

        [HttpPut("Edit Docente")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult<Estudiante>> EditarDocente(DocenteDto dto, string UUID)
        {
            var user= await _userService.GetDocentebyGUID(UUID);
            if(user==null) return NotFound();
            await _userService.EditDocente(dto,UUID);
            return Ok(user);
        }
    }
}