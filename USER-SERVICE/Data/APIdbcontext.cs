using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Models;


namespace USER_SERVICE.Data
{
    public class APIdbcontext : DbContext
    {
        public APIdbcontext(DbContextOptions<APIdbcontext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Docente> Docentes {get; set;}
    }
}