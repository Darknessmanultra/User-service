using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USER_SERVICE.DTOs
{
    public class EstudianteDto
    {
        public string Nombre {get; set;}
        public string Email {get; set;}
    }

    public class DocenteDto
    {
        public string Nombre {get; set;}
        public string Email {get; set;}
    }

    public class DocenteRegistroDto
    {
        public string Nombre {get; set;}

    }

    public class DocenteLoginDto
    {
        public string Email {get; set;}
        public string Password {get; set;}
    }
}