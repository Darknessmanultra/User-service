using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USER_SERVICE.Models
{
    public class User
    {
        public int UUID {get; set;}
        public string Nombre {get; set;}
        public string Email {get; set;}
    }

    public class Estudiante: User
    {

    }

    public class Docente: User
    {
        public string PasswordHash{get; set;}
    }

    public class Admin: User
    {
        public string Password{get;set;}
    }
}