using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Models;
using USER_SERVICE.Data;

namespace USER_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly APIdbcontext _context;
        
        public UserController(APIdbcontext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        [HttpGet("{UUID}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int UUID)
        {
            var estudiante = await _context.Estudiantes.FindAsync(UUID);

            if (estudiante==null)
            {
                return NotFound();
            }
            return estudiante;
        }
    }
}