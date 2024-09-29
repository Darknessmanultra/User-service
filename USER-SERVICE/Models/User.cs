using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USER_SERVICE.Models
{
    public class User
    {
        public string UUID {get; set;}
        public string Nombre {get; set;}
        [EmailAddress]
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