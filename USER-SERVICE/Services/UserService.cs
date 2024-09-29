using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Data;
using USER_SERVICE.DTOs;
using USER_SERVICE.Models;


namespace USER_SERVICE.Services
{
    public class UserService
    {
        private readonly APIdbcontext _context;

        public UserService(APIdbcontext context)
        {
            _context=context;
        }

        public async Task<Estudiante> RegistrarEstudiante(EstudianteDto dto)
        {
            if(await _context.Estudiantes.AnyAsync(u=>u.Nombre==dto.Nombre||u.Email==dto.Email))
            {
                throw new Exception("Don't roll the Ks");
            }

            var GUIDO = Guid.NewGuid().ToString();

            var estudiante = new Estudiante
            {
                Nombre=dto.Nombre,
                Email=dto.Email,
                UUID=GUIDO
            };
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<Docente> RegistrarDocente(DocenteDto dto)
        {
            if(await _context.Docentes.AnyAsync(u=>u.Nombre==dto.Nombre||u.Email==dto.Email))
            {
                throw new Exception("Don't roll the Ks");
            }

            var PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Nombre);
            var GUIDO = Guid.NewGuid().ToString();

            var docente = new Docente
            {
                Nombre=dto.Nombre,
                Email=dto.Email,
                PasswordHash=PasswordHash,
                UUID=GUIDO
            };

            _context.Docentes.Add(docente);
            _context.Users.Add(docente);
            await _context.SaveChangesAsync();

            return docente;
        }

        public async Task<User> Login(DocenteLoginDto dto)
        {
            var user = await _context.Docentes.SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Email o contraseña inválida");
            }

            return user;
        }
        
        public async Task<User> GetUserbyGUID(string UUID)
        {
            return await _context.Estudiantes.FindAsync(UUID);
        }
    }
}